(function(app) {

    'use strict';

    function indexController($scope, $location, apiService, notificationService) {
        $scope.pageClass = "page-home";
        $scope.loadingMovies = true;
        $scope.loadingGenres = true;
        $scope.isReadOnly = true;

        $scope.latestMovies = [];

        function moviesLoadCompleted(result) {
            $scope.latestMovies = result.data;
            $scope.loadingMovies = false;
        }


        function movieLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function genresLoadCompleted(result) {
            var genres = result.data;
            Morris.Bar({
                element: "genres-bar",
                data: genres,
                xkey: "Name",
                ykeys: ["NumberOfMovies"],
                labels: ["Number Of Movies"],
                barRatio: 0.4,
                xLabelAngle: 55,
                hideHover: "auto",
                resize: 'true'
            });
            $scope.loadingGenres = false;
        }

        function genresLoadFailed(response) { notificationService.displayError(response.data); }

        apiService.get('/api/movies/latest', null, moviesLoadCompleted, movieLoadFailed);
        apiService.get("/api/genres/", null, genresLoadCompleted, genresLoadFailed);

    };


    indexController.$inject = ['$scope', '$location', 'apiService', 'notificationService'];

    app.controller('indexController', indexController);

})(angular.module('homeCinema'));