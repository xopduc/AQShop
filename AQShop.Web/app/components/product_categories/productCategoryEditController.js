/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ["$scope", "apiService", "notificationService","$state", "$stateParams","commonService"];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService)
    {
        $scope.productCategory= {
            CreatedDate: new Date(),          
            Status: true
        }

        $scope.parentCategory = [];
        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle()
        {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadParentCategory()
        {
            apiService.get("/api/productCategory/GetAllParentProductCategory", null, function(result) {
                $scope.parentCategory = result.data;
            }, function()
            {
                Console.log("can not get list parent of product category");
            });
        }

        $scope.UpdateProductCategory = UpdateProductCategory;

        function loadProductCategoryById() {
            apiService.get("/api/productCategory/GetById/" + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function () {
                notificationService.displayError("can not get list parent of product category");
            });
        }

        function UpdateProductCategory(){
            apiService.put("/api/productCategory/update", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật.");
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError("Thêm mới không thành công");
            });
        }

        
       
        loadParentCategory();
        loadProductCategoryById();
    }

})(angular.module("aqshop.product_categories"));