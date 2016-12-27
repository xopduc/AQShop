/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ["$scope", "apiService", "notificationService","$state", "$stateParams","commonService"];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService)
    {
        $scope.product= {
            CreatedDate: new Date(),          
            Status: true
        }
        $scope.moreImages = [];
        $scope.parentCategory = [];
        $scope.getSeoTitle = getSeoTitle;
        $scope.ckeditorOptions = {
            languague: 'en',
            height: '200px'
        }

        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    if ($scope.moreImages.indexOf(fileUrl) < 0) {
                        $scope.moreImages.push(fileUrl);
                    }
                })

            }

            finder.popup();
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })

            }

            finder.popup();
        }

        function getSeoTitle()
        {           
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadParentCategory()
        {
            apiService.get("/api/productCategory/GetAllParentProductCategory", null, function (result) {
                $scope.parentCategory = result.data;
            }, function()
            {
                Console.log("can not get list parent of product category");
            });
        }

        $scope.UpdateProduct = UpdateProduct;

        function loadProductById() {
            apiService.get("/api/product/GetById/" + $stateParams.id, null, function (result) {
                $scope.product = result.data;
            }, function () {
                notificationService.displayError("can not get list parent of product");
            });
        }

        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.put("/api/product/update", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật.");
                $state.go('products');
            }, function (error) {
                notificationService.displayError("Thêm mới không thành công");
            });
        }

        
       
        loadParentCategory();
        loadProductById();
    }

})(angular.module("aqshop.products"));