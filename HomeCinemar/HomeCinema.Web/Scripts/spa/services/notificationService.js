(function(app) {
    'use strict';

    var notificationService = function() {

        toastr.options = {
            'debug': false,
            'positionClass': 'toast-top-right',
            'onclick': null,
            'fadeIn': 300,
            'fadeOut': 1000,
            'timeOut': 3000,
            'extendedTimeOut': 1000
        };


        function displaySuccess(message) {
            toastr.success(message);
        };

        function displayError(error) {
          if (Array.isArray(error)) {
              error.forEach(function(err) {
                  toastr.error(err);
              });
          } else {
              toastr.error(error);
          };
        };

        function displayWarning(message) {
            toastr.warning(message);
        }

        function displayInfo(message) {
            toastr.info(message);
        }

        var service = {
            displaySuccess: displaySuccess,
            displayError: displayError,
            dispalyWarning: displayWarning,
            dispalyInfo: displayInfo
        };

        return service;

        

    };

    app.factory('notificationService', notificationService);
})(angular.module('common.core'));