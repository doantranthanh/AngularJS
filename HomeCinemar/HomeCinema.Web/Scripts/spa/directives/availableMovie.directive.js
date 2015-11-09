(function(app) {
    'use strict';

    function availabeleMovie() {
        return {
            restrict: 'E',
            templateUrl: "/Scripts/spa/directives/availableMovie.html",
            link: function($scope, $element, $attr) {
                $scope.getAvailableClass = function() {
                    if ($attrs.isAvailable === 'true') 
                        return 'label label-success' 
                    else return 'label label-danger'
                }; 

                $scope.getAvailability = function() {
                    if ($attrs.isAvailable === 'true') 
                        return 'Available!'
                    else return 'Not Available'
                };
            }
        }
    }


})(angular.module('common.ui'));