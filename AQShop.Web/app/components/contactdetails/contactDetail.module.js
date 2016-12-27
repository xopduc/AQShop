/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module("aqshop.contactDetails", ['aqshop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('contactDetails', {
            url: '/contactDetails',
            parent: 'base',
            templateUrl: "/app/components/contactDetails/contactDetailListView.html",
            controller: "contactDetailListController"
        }).state('add_contactDetail', {
            url: '/add_contactDetail',
            parent: 'base',
            templateUrl: "/app/components/contactDetails/contactDetailAddView.html",
            controller: "contactDetailAddController"
        }).state('edit_contactDetail', {
            url: '/edit_contactDetail/:id',
            parent: 'base',
            templateUrl: "/app/components/contactDetails/contactDetailEditView.html",
            controller: "contactDetailEditController"
        });
    }
})();