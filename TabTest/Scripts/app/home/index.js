﻿$(function() {
    $("#jqxTabs").jqxTabs({autoHeight: true});

    $("#tab1Items").selectmenu();

    //$('#tablist').Tabify();

    function viewModels() {

        this.Tab1Model = {
            @Html.KnockoutModelFor(Model.Tab1Model, true)
        }

        this.Tab2Model = {
            @Html.KnockoutModelFor(Model.Tab2Model, true)
        }
    }

    ko.applyBindings(new viewModels());

    var categorysource =
    {
        datatype: "json",
        url: '@Url.Action("Categories", "Northwind")',
        root: 'Categories'
    };

    var categoryAdapter = new $.jqx.dataAdapter(categorysource);

    var source =
    {
        datatype: "json",
        datafields: [
            { name: 'ProductID', type: 'number' },
            { name: 'Name', type: 'string' },
            { name: 'Color', type: 'string' },
            { name: 'StandardCost', type: 'number' },
            { name: 'ProductNumber', type: 'string' },
            { name: 'ProductCategoryID', type: 'number' },
            { name: 'ProductCategoryName', type: 'string' }
        ],
        url: '@Url.Action("AllProducts", "Northwind")',
        root: 'Rows',
        beforeprocessing: function(data) {
            source.totalrecords = data.TotalRows;
        },
        sort: function() {
            // update the grid and send a request to the server.
            $("#jqxgrid2").jqxGrid('updatebounddata');
        },
        filter: function() {
            // update the grid and send a request to the server.
            $("#jqxgrid2").jqxGrid('updatebounddata');
        },
        updaterow: function(rowid, rowdata, commit) {
            // synchronize with the server - send update command
            var data = "ProductCategoryID=" + rowdata.ProductCategoryID + "&ProductID=" + rowdata.ProductID + "&Name=" + rowdata.Name + "&ProductNumber=" + rowdata.ProductNumber + "&StandardCost=" + rowdata.StandardCost + "&Color=" + rowdata.Color;
            $.ajax({
                dataType: 'json',
                url: '@Url.Action("UpdateProduct", "Northwind")',
                data: data,
                type: 'POST',
                success: function (returndata, status, xhr) {
                    if (returndata.success)
                        // update command is executed.
                        commit(true);
                    else {
                        alert('bad');
                        commit(false);
                    }
                },
                error: function() {
                    // cancel changes.
                    commit(false);
                }
            });
        }
    };

    var dataAdapter = new $.jqx.dataAdapter(source, {
        formatData: function(data) {
            console.log(data);
            data.nameSearch = $("#searchField").val();
            return data;
        }
    });

    $("#jqxgrid2").jqxGrid(
    {
        width: 850,
        autoheight: true,
        theme: 'energyblue',
        source: dataAdapter,
        pageable: true,
        sortable: true,
        filterable: true,
        virtualmode: true,
        editable: true,
        editmode: 'dblclick',
        rendergridrows: function() {
            return dataAdapter.records;
        },
        updatefilterconditions: function(type, defaultconditions) {
            var stringcomparisonoperators = [
                'EMPTY', 'NOT_EMPTY', 'CONTAINS_CASE_SENSITIVE',
                'DOES_NOT_CONTAIN_CASE_SENSITIVE', 'STARTS_WITH_CASE_SENSITIVE',
                'ENDS_WITH_CASE_SENSITIVE', 'EQUAL_CASE_SENSITIVE', 'NULL', 'NOT_NULL'
            ];
            var numericcomparisonoperators = ['EQUAL', 'NOT_EQUAL', 'LESS_THAN', 'LESS_THAN_OR_EQUAL', 'GREATER_THAN', 'GREATER_THAN_OR_EQUAL', 'NULL', 'NOT_NULL'];
            var datecomparisonoperators = ['EQUAL', 'NOT_EQUAL', 'LESS_THAN', 'LESS_THAN_OR_EQUAL', 'GREATER_THAN', 'GREATER_THAN_OR_EQUAL', 'NULL', 'NOT_NULL'];
            var booleancomparisonoperators = ['EQUAL', 'NOT_EQUAL'];
            switch (type) {
                case 'stringfilter':
                    return stringcomparisonoperators;
                case 'numericfilter':
                    return numericcomparisonoperators;
                case 'datefilter':
                    return datecomparisonoperators;
                case 'booleanfilter':
                    return booleancomparisonoperators;
            }
        },
        showtoolbar: true,
        rendertoolbar: function(toolbar) {
            var me = this;
            var container = $("<div style='margin: 5px;'></div>");
            var span = $("<span style='float: left; margin-top: 5px; margin-right: 4px;'>Search Products: </span>");
            var input = $("<input class='jqx-input jqx-widget-content jqx-rc-all' id='searchField' type='text' style='height: 23px; float: left; width: 223px;' />");


            toolbar.append(container);
            container.append(span);
            container.append(input);
            container.append('<input id="addrowbutton" style="float:right;" type="button" value="New Product" />');
            $("#addrowbutton").jqxButton();

            // create new row.
            $("#addrowbutton").on('click', function() {
                //var row = {};

                //row["ProductName"] = "New";

                //$("#jqxgrid2").jqxGrid('addrow', null, row);

                window.location = '@Url.Action("Add", "Product")';
            });


            //if (theme != "") {
            //    input.addClass('jqx-widget-content-' + theme);
            //    input.addClass('jqx-rc-all-' + theme);
            //}
            var oldVal = "";
            input.on('keydown', function(event) {
                if (input.val().length >= 2) {
                    if (me.timer) {
                        clearTimeout(me.timer);
                    }
                    if (oldVal != input.val()) {
                        me.timer = setTimeout(function() {
                            $("#jqxgrid2").jqxGrid('updatebounddata');
                        }, 1000);
                        oldVal = input.val();
                    }
                } else {
                    $("#jqxgrid2").jqxGrid('updatebounddata');
                }
            });
        },
        autoshowfiltericon: true,
        pagesize: 20,
        pagesizeoptions: ['10', '20', '50'],
        columnsresize: true,
        columnsreorder: true,
        columns: [
            { datafield: 'ProductID', hidden: true },
            { text: 'Product Name', datafield: 'Name', width: 200, cellsalign: 'center' },
            {
                text: 'Color',
                datafield: 'Color',
                width: 70,
                cellsalign: 'right'
            },
            {
                text: 'Standard Cost',
                datafield: 'StandardCost',
                cellsformat: 'c2',
                width: 130,
                columntype: 'numberinput',
                cellsalign: 'right',
                initeditor: function(row, cellvalue, editor) {
                    editor.jqxNumberInput({ decimalDigits: 2 });
                }
            },
            { text: 'Product Number', datafield: 'ProductNumber', width: 150, cellsalign: 'center' },
            {
                text: 'Product Category',
                datafield: 'ProductCategoryID',
                displayfield: 'ProductCategoryName',
                minwidth: 110,
                cellsalign: 'center',
                columntype: 'dropdownlist',
                createeditor: function(row, value, editor) {
                    editor.jqxDropDownList({ source: categoryAdapter, displayMember: 'Name', valueMember: 'ProductCategoryID' });
                }
            }
        ]
    }).bind('bindingcomplete', function() {
        var localizationobj = {};
        var filterstringcomparisonoperators = [
            'empty', 'not empty', 'contains(match case)',
            'does not contain(match case)', 'starts with(match case)',
            'ends with(match case)', 'equal(match case)', 'null', 'not null'
        ];
        var filternumericcomparisonoperators = ['equal', 'not equal', 'less than', 'less than or equal', 'greater than', 'greater than or equal', 'null', 'not null'];
        var filterdatecomparisonoperators = ['equal', 'not equal', 'less than', 'less than or equal', 'greater than', 'greater than or equal', 'null', 'not null'];
        var filterbooleancomparisonoperators = ['equal', 'not equal'];

        localizationobj.filterstringcomparisonoperators = filterstringcomparisonoperators;
        localizationobj.filternumericcomparisonoperators = filternumericcomparisonoperators;
        localizationobj.filterdatecomparisonoperators = filterdatecomparisonoperators;
        localizationobj.filterbooleancomparisonoperators = filterbooleancomparisonoperators;

        // apply localization.
        $("#jqxgrid2").jqxGrid('localizestrings', localizationobj);
    });
