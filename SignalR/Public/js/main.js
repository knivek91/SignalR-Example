var jq = $(document);
var connection = $.connection.tableHub;
var lstPeople = [];

// Init Table
(function (jq) {

    var $table = jq.find('#tblPeople');

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

})(jq);

// Client's methods
connection.client.receiveNewRow = function (person) {
    
    var $table = jq.find('#tblPeople');
    lstPeople.push(JSON.parse(person));
    $table.bootstrapTable('load', lstPeople);

    toastr.success('New Row', 'Someone Insert a new Row');
};

// Server's Methods
jq.find('#btnInsert').click(function () {

    var newRow = { I: 1, Name: 'New Row', Age: 100 };

    connection.server.insertRow(newRow);

});

// Stablish the connection
$.connection.hub.start();