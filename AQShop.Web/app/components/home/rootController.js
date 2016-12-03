/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app)
{
    app.controller('rootController', rootController);
    rootController.$inject= ['$scope','$state','loginService','authenticationService','authData'];
    function rootController($scope, $state, loginService, authenticationService,authData)
    {
        $scope.logOut = logOut;
        function logOut()
        {
           
            loginService.logOut();
          
            $state.go('login');
        }

        $scope.authentication = authData.authenticationData;

        //authenticationService.validateRequest();
    }

})(angular.module('aqshop'))