/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('contactDetailEditController',contactDetailEditController);
    contactDetailEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"];

    function contactDetailEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
             
     

        $scope.UpdateContactDetail = UpdateContactDetail;

        function loadContactDetailById() {
            apiService.get("/api/contactDetail/GetById/" + $stateParams.id, null, function (result) {
                $scope.contactDetail = result.data;
            }, function () {
                notificationService.displayError("Không thể load page");
            });
        }

        function UpdateContactDetail() {   
            apiService.put("/api/contactDetail/update", $scope.contactDetail, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật.");
                $state.go('contactDetails');
            }, function (error) {
                notificationService.displayError("Thêm mới không thành công");
            });
        }       
              
        loadContactDetailById();
    }

})(angular.module("aqshop.contactDetails"));