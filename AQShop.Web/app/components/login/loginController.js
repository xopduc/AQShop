/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app)
{
    app.controller('loginController', loginController);
    loginController.$inject = ["$scope", "loginService", "$injector","notificationService"];
    function loginController($scope, loginService, $injector, notificationService)
    {
        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.LoginInSubmit = function () {
            loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                if (response != null && response.error != undefined) {
                    notificationService.displayError("Đăng nhập không đúng.");
                }
                else {
                    var stateService = $injector.get('$state');
                    stateService.go('home');
                }
            });
        };
    }
})(angular.module("aqshop"))