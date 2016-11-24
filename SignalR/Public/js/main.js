var jq = $(document);
var connection = $.connection.tableHub;
toastr.options.positionClass = "toast-top-left";
toastr.options.closeButton = true;

var person = function () {

    var lstPeople = [];

    return {

        createTable: function createTable($table) {

            $table.bootstrapTable({
                data: lstPeople || [],
                cache: false,
                height: 510,
                striped: false,
                pagination: true,
                pageSize: 10,
                pageList: [10, 50, 100, 250, 350, 500],
                search: true,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 2,
                clickToSelect: true,
                rowAttributes: function (row, index) { return { 'data-id': row.ID }; },
                columns: [{
                    field: 'ID',
                    title: 'ID',
                }, {
                    field: 'Name',
                    title: 'Name',
                }, {
                    field: 'Age',
                    title: 'Age',
                }]
            });

            return true;

        }
        , insertNewPerson: function insertNewPerson(person) {
            connection.server.insertRow(person);
        }
        , addItemToList: function addItemToList(person) {
            lstPeople.push(person);
            return true;
        }
        , updateItemList: function updateItemList(person) {

            $.grep(lstPeople, function (item, index) {
                if (item.ID == person.ID) {
                    item.Name = person.Name;
                    item.Age = person.Age;
                }
            });

            return true;
        }
        , removeItemList: function removeItemList(id) {
            lstPeople = lstPeople.filter(function (item, index) { return item.ID != id; });
            return true;
        }
        , reloadTable: function reloadTable($table) {
            $table.bootstrapTable('load', lstPeople);
            return true;
        }
        , getInputs: function getInputs() {
            return {
                Name: name = jq.find('input[name="txtName"]'),
                Age: age = jq.find('input[name="txtAge"]')
            }
        }
        , getInputsData: function getInputsData() {
            return {
                Name: name = jq.find('input[name="txtName"]').val(),
                Age: age = jq.find('input[name="txtAge"]').val()
            }
        }
        , updateItem: function (person) {
            connection.server.updateRow(person);
        }
        , setInfoInInputs: function setInfoInInputs(id) {

            var $inputs = this.getInputs();
            var arrFiltered = lstPeople.filter(function (item, index) { return item.ID == id; })[0];

            $inputs.Name.val(arrFiltered.Name);
            $inputs.Age.val(arrFiltered.Age);

            return true;
        }
        , deleteItem: function deleteItem(id) {
            connection.server.removeRow(id);
        }
    }

};

// Stablish the connection
$.connection.hub.start();

// Literal Object
var Person = person();

jq.ready(function () {

    var $table = jq.find('#tblPeople');
    Person.createTable($table);

});

// Server Method
setTimeout(function () {
    connection.server.initSession();
}, 1000);

// Client's methods
connection.client.receiveNewRow = function (person) {

    if (person != '') {

        var $table = jq.find('#tblPeople');

        var newItem = JSON.parse(person);

        if (Person.addItemToList(newItem)) {
            if (Person.reloadTable($table)) {
                toastr.info('ID: ' + newItem.ID + ' Name: ' + newItem.Name + ' Age: ' + newItem.Age, 'New Row');
            }
        }
    }

}

connection.client.receiveUpdatedRow = function (person) {

    if (person != '') {
        var newItem = JSON.parse(person);
        if (Person.updateItemList(newItem)) {
            var $table = jq.find('#tblPeople');
            if (Person.reloadTable($table)) {
                toastr.info('ID: ' + newItem.ID + ' Name: ' + newItem.Name + ' Age: ' + newItem.Age, 'Updated Row');
            }
        }
    }
}

connection.client.receiveRemovedRow = function (id) {

    if (person != '') {
        
        if (Person.removeItemList(id)) {
            var $table = jq.find('#tblPeople');
            if (Person.reloadTable($table)) {
                toastr.info('ID: ' + id, 'Deleted Row');
            }
        }
    }
}

connection.client.newUserConnected = function (msg) {
    toastr.success(msg);
}

connection.client.newUserConnected = function (msg) {
    toastr.error(msg);
}

connection.client.sessionEnd = function () {
    toastr.warning('Your session expired. Go to Login.');
}

// Server's Methods
jq.find('#btnInsert').click(function () {

    var name = jq.find('input[name="txtName"]').val();
    var age = jq.find('input[name="txtAge"]').val();

    var newRow = { ID: Math.round(Math.random() * 100), Name: name, Age: age };

    Person.insertNewPerson(newRow);

});

jq.find('#btnUpdate').click(function () {

    var id = jq.find('#tblPeople tbody tr.info').attr('data-id');
    var name = jq.find('input[name="txtName"]').val();
    var age = jq.find('input[name="txtAge"]').val();

    var newRow = { ID: id, Name: name, Age: age };

    Person.updateItem(newRow);

});

jq.find('#btnDelete').click(function () {

    var id = jq.find('#tblPeople tbody tr.info').attr('data-id');
    Person.deleteItem(id);

});

jq.on('click', '#tblPeople tbody tr', function () {

    var $selectedRow = jq.find('#tblPeople tbody tr.info');
    var ID = $(this).attr('data-id');

    if (typeof $selectedRow !== typeof undefined && typeof ID !== typeof undefined) {
        $selectedRow.removeClass('info');

        $(this).addClass('info');

        Person.setInfoInInputs(ID);
    }

});