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
      $http.get(config.apiUrl + '/prices')
      .then(function(response) {
          var prices = response.data;
          $scope.funds = getFunds(prices);
          $scope.model = getModel($scope.funds, prices);
      });
    });

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
          model[price.date] = {
            date: price.date,
            funds: {}
          };

          funds.forEach(function(fund) {
            model[price.date].funds[fund] = {
              fund: fund,
              value: null
            };
          });
        }

        model[price.date].funds[price.fund].value = price.value;
      });

      return model;
    }
})();