const app = angular.module('contactListApp', []);

app.controller('ContactsController', function ContactsController($scope, $http) {
    $scope.contacts = [];

    $http.get('api/contacts')
        .then(function (response) {
            $scope.contacts = response.data;
        });

    $scope.deleteContact = function (contact) {
        const idx = $scope.contacts.indexOf(contact);
        if (idx >= 0) {
            $scope.contacts.splice(idx, 1);
        }
    };

    $scope.contact = {
        name: '',
        email: ''
    };

    $scope.showErrors = false;
    $scope.error = '';

    $scope.submitForm = function () {
        if (!$scope.form.$valid) {
            alert('form is not valid')
        }

        $scope.error = '';
        $scope.showErrors = true;

        $http.post('api/contacts', $scope.contact)
            .then(function () {
                $scope.contacts.push(
                    {
                        name: $scope.contact.name,
                        email: $scope.contact.email
                    });
            })
            .catch(function (error) {
                console.log(error);
                $scope.error = error.data;
                $scope.showErrors = true;
            })
    }
});
