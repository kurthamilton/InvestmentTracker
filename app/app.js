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
      $http
        .get(config.apiUrl + '/prices')
        .then(function(response) {
          bindResponse($scope, response, deleteDate);
        });

      function deleteDate(date) {

        for (var fund in $scope.model[date].funds) {
          $http
            .delete(config.apiUrl + '/prices/' + $scope.model[date].funds[fund].id)
            .then(function(response) {
              bindResponse($scope, response, deleteDate);
            });
        }
      };
    });

    function bindResponse($scope, response, deleteDate) {
      var prices = response.data;
      $scope.funds = getFunds(prices);
      $scope.model = getModel($scope.funds, prices, deleteDate);
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

    function getModel(funds, prices, deleteDate) {
      var model = {};

      prices.forEach(function(price) {
        if (!model.hasOwnProperty(price.date)) {
          model[price.date] = {
            date: price.date,
            delete: function() {
              deleteDate(price.date);
            },
            funds: {}
          };

          funds.forEach(function(fund) {
            model[price.date].funds[fund] = {
              fund: fund,
              value: null
            };
          });
        }

        var priceModel = model[price.date].funds[price.fund];
        priceModel.id = price.id;
        priceModel.value = price.value;
      });

      return model;
    }
})();