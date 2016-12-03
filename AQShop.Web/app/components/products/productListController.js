/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller("productListController", productListController);
    productListController.$inject = ["$scope", "apiService","notificationService","$state","$mdDialog"];

    function productListController($scope, apiService, notificationService, $state, $mdDialog) {
        $scope.productList = [];
        $scope.page = 0;
        $scope.pagesCount = 2;
        $scope.getAllProduct = getAllProduct;
        $scope.keyword = '';

        $scope.items = [];

        $scope.deleteMulti = deleteMulti;

        $scope.search = search;

        $scope.deleteById = deleteById;
       
        $scope.selected = [];

        $scope.toggle = function (item, list) {
            var idx = list.indexOf(item);
            if (idx > -1) {
                list.splice(idx, 1);
            }
            else {
                list.push(item);
            }
            console.log($scope.selected);
        };

        $scope.exists = function (item, list) {
            return list.indexOf(item) > -1;
        };

        $scope.isAllowDeleteMulti = function()
        {
            return ($scope.selected.length > 0);
        }

        $scope.isChecked = function () {
            return $scope.selected.length === $scope.items.length;
        };

        $scope.toggleAll = function () {
            if ($scope.selected.length === $scope.items.length) {
                $scope.selected = [];
            } else if ($scope.selected.length === 0 || $scope.selected.length > 0) {
                $scope.selected = $scope.items.slice(0);
            }
        };
        
        function deleteById(id)
        {
            var confirm = $mdDialog.confirm()
                   .title('Xac nhan')
                   .textContent('Ban co chac muon xoa ban ghi nay khong?')
                   .ariaLabel('')                  
                   .ok('Dong y')
                   .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {
                var config = {
                    id: id
                };
                apiService.del("api/product/DeleteById?id=" + id, null, function (result) {
                    notificationService.displaySuccess("Bản ghi " + result.data.Name + " đã được xóa thành công");
                    search();

                }, function (failure) {
                    notificationService.displayWarning("Đã xảy ra lỗi khi xóa bản ghi");
                });
            }, function () {
                return false;
            });          
          
        }

        function deleteMulti() {
            var confirm = $mdDialog.confirm()
                   .title('Xac nhan')
                   .textContent('Ban co chac muon xoa cac ban ghi da chon khong?')
                   .ariaLabel('')
                   .ok('Dong y')
                   .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {                
                apiService.del("api/product/DeleteMulti?ids=" + $scope.selected.toString(), null, function (result) {
                    notificationService.displaySuccess("Da xoa thanh cong cac ban ghi");
                    search();

                }, function (failure) {
                    notificationService.displayWarning("Đã xảy ra lỗi khi xóa cac bản ghi");
                });
            }, function () {
                return false;
            });

        }

        function search()
        {
            getAllProduct();
            if ($scope.totalCount == 0) {
                notificationService.displayWarning("Khong tim thay ban ghi nao");
            }
            else {
                notificationService.displaySuccess("Tìm thấy " + $scope.totalCount + " bản ghi");
            }
        }

        function getAllProduct(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page : page,
                    pageSize : 2
                }
            }
            apiService.get('/api/product/getall', config, function (result) {
              
                $scope.productList = result.data.Items;

                result.data.Items.forEach(function(item){
                    $scope.items.push(item.ID);
                });

                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                notificationService.displayError("Đã xảy ra lỗi");
            });

        }
        $scope.getAllProduct();
    }
})(angular.module("aqshop.products"));