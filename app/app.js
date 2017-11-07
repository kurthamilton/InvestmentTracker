(function() {
  // load config
  var config = window.__config;

  if (config === undefined) {
    alert('config.js not set up. Please see readme.');
    return;
  }

  var app = angular.module('app', []);

  // setup controller
  app
    .controller('pricesController', function($scope, $http) {
      loadPrices();

      // scraper
      $scope.scraper = getScraper();

      function bindResponse(response) {
        var prices = response.data;
        $scope.funds = getFunds(prices);
        $scope.model = getModel($scope.funds, prices);
      }

      function deleteDate(date) {
        var deleteRequests = [];

        var funds = $scope.model[date].funds;
        for (var fund in funds) {
          if (funds.hasOwnProperty(fund) && funds[fund].id) {
            var deleteUrl = config.apiUrl + '/prices/' + funds[fund].id;
            var deleteRequest = $http.delete(deleteUrl);
            deleteRequests.push(deleteRequest);
          }
        }

        Promise
          .all(deleteRequests)
          .then(function() {
            loadPrices();
          });
      }

      function getFunds(prices) {
        var funds = [];
        prices.forEach(function(price) {
          if (!funds.includes(price.fund)) {
            funds.push(price.fund);
          }
        });

        return funds;
      }

      function getModel(funds, prices) {
        var model = {};

        prices.forEach(function(price) {
          if (!model.hasOwnProperty(price.date)) {
            var dateModel = {
              date: price.date,
              delete: function() {
                deleteDate(price.date);
              },
              funds: {}
            };

            funds.forEach(function(fund) {
              dateModel.funds[fund] = {
                fund: fund,
                value: null
              };
            });

            model[price.date] = dateModel;
          }

          var priceModel = model[price.date].funds[price.fund];
          priceModel.id = price.id;
          priceModel.value = price.value;
        });

        return model;
      }

      function getScraper() {
        var data = {
          intervalDays: 1
        };

        return {
          from: window.localStorage['scraper.from'],
          intervalDays: parseInt(window.localStorage['scraper.intervalDays']) || 1,
          passPhrase: window.localStorage['scraper.passPhrase'],
          to: window.localStorage['scraper.to'],
          scrape: function() {

            var scraperUrl = config.apiUrl + '/prices/scrape?' +
                                            'investment=' + encodeURIComponent(config.scraper.investment) +
                                            '&url=' + encodeURIComponent(config.scraper.url) +
                                            '&username=' + encodeURIComponent(config.scraper.username) +
                                            '&password=' + encodeURIComponent(config.scraper.password) +
                                            '&passPhrase=' + encodeURIComponent($scope.scraper.passPhrase) +
                                            '&from=' + $scope.scraper.from +
                                            '&to=' + $scope.scraper.to +
                                            '&intervalDays=' + $scope.scraper.intervalDays;
            $http
              .post(scraperUrl)
              .then(function(response) {
                bindResponse(response);
              });

            window.localStorage['scraper.passPhrase'] = $scope.scraper.passPhrase;
            window.localStorage['scraper.from'] = $scope.scraper.from;
            window.localStorage['scraper.intervalDays'] = $scope.scraper.intervalDays;
            window.localStorage['scraper.to'] = $scope.scraper.to;
          },
        };
      }

      function loadPrices() {
        $http
          .get(config.apiUrl + '/prices')
          .then(function(response) {
            bindResponse(response);
          });
      }
    });
})();