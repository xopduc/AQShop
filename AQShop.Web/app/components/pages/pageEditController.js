/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('pageEditController',pageEditController);
    pageEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"];

    function pageEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
              
        $scope.getSeoTitle = getSeoTitle;
        $scope.ckeditorOptions = {
            languague: 'en',
            height: '200px'
        }
             
       
        function getSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }              

        $scope.UpdatePage = UpdatePage;

        function loadPageById() {
            apiService.get("/api/page/GetById/" + $stateParams.id, null, function (result) {
                $scope.product = result.data;
            }, function () {
                notificationService.displayError("Không thể load page");
            });
        }

        function UpdatePage() {   
            apiService.put("/api/page/update", $scope.page, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật.");
                $state.go('pages');
            }, function (error) {
                notificationService.displayError("Thêm mới không thành công");
            });
        }



        loadParentCategory();
        loadProductById();
    }

})(angular.module("aqshop.pages"));