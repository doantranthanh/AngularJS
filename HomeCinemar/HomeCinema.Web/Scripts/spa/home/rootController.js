(function(app) {
    'use strict';

    function rootController($scope) {
        $scope.userData = {};

        $scope.userData.displayUserInfo = displayUserInfo;

        $scope.logout = logout;

        function displayUserInfo() {

        };

        function logout() {
            $location.path('/');
        }

        $scope.userData.displayUserInfo();
    };


    rootController.$inject = ['$scope', '$location', 'membershipService', '$rootScope'];

    app.controller('rootController', rootController);

    
})(angular.module('homeCinema'));