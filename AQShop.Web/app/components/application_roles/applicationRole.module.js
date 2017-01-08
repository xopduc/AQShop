﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module("aqshop.application_roles", ['aqshop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('application_roles', {
            url: '/application_roles',
            parent: 'base',
            templateUrl: "/app/components/application_roles/applicationRoleListView.html",
            controller: "applicationRoleListController"
        }).state('add_application_role', {
            url: '/add_application_role',
            parent: 'base',
            templateUrl: "/app/components/application_roles/applicationRoleAddView.html",
            controller: "applicationRoleAddController"
        }).state('edit_application_role', {
            url: '/edit_application_role/:id',
            parent: 'base',
            templateUrl: "/app/components/application_roles/applicationRoleEditView.html",
            controller: "applicationRoleEditController"
        });
    }
})();