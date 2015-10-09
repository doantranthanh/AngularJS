﻿(function () {
    'use strict';
    angular.module('homeCinema', ['common.core', 'common.ui'])
    .config(config);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/customers", {
                templateUrl: "scripts/spa/customers/index.html",
                controller: "customerCtrl"
            })
            .when("/customers/register", {
                templateUrl: "scripts/spa/customers/register.html",
                controller: "customerRegCtrl"
            })
            .when("/movies", {
                templateUrl: "scripts/spa/movies/index.html",
                controller: "moviesCtrl"
            })
            .when("/movies/add", {
                templateUrl: "scripts/spa/movies/add.html",
                controller: "movieAddCtrl"
            })
            .when("/movies/:id", {
                templateUrl: "scripts/spa/movies/details.html",
                controller: "movieDetailsCtrl"
            })
            .when("/movies/edit/:id", {
                templateUrl: "scripts/spa/movies/edite/html",
                controller: "movieEditCtrl"
            })
            .when("/rental", {
                templateUrl: "scripts/spa/rental/index.html",
                controller: "rennStatsCtrl"
            }).otherwise({ redirectTo: "/" });
    }
})()