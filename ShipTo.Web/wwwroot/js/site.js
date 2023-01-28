// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/************************************************* DataTable *************************************************************************/
function AddDataTable(tableId, GetURL, columnsArr) {
    $('#' + tableId + ' thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#' + tableId + ' thead');

    var table = $('#' + tableId).dataTable({
        dom: 'Bfrtip',
        "ajax": {
            url: GetURL,
            type: "GET",
            dataSrc: function (resdata) {

                return resdata;
            }
        },
        "autoWidth": true,
        columns: columnsArr,
        columnDefs: [{
            "defaultContent": "",
            "targets": "_all"
        }],
        buttons: [
            'pageLength'
            ,
            {
                extend: 'excel',
                text: 'استخراج كملف إكسل',
                exportOptions: {
                    columns: ':visible:not(.notExportCol)'
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        orderCellsTop: true,
        "bLengthChange": true,
       /* stateSave: true,*/
        initComplete: function () {
            var api = this.api();
            // For each column
            api
                .columns(':visible')
                .eq(0)
                .each(function (colIdx) {
                    //Except last column that have Actions
                    /*  alert(this.columns(':visible').count());  */
                    /* alert(colIdx);*/
                    if (colIdx == this.columns(':visible').count())
                        return;
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input  type="text" style="width: ' + cell.width() + 'PX; max-width:150px"   />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('change', function (e) {
                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();
                        })
                        .on('keyup', function (e) {
                            e.stopPropagation();

                            $(this).trigger('change');
                            $(this)
                                .focus()[0]
                            /*.setSelectionRange(cursorPosition, cursorPosition);*/
                        });
                });
        },
    });
    return table;
}

function AddDataTable_WithMultiSelect(tableId, GetURL, columnsArr, ButtonsArr = [], footerCallback = null) {

    $('#' + tableId + ' thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#' + tableId + ' thead');

    var table = $('#' + tableId).DataTable({
        dom: 'Bfrtip',
        "ajax": {
            url: GetURL,
            type: "GET",
            dataSrc: function (resdata) {

                return resdata;
            }
        },
        "autoWidth": true,
        columns: columnsArr,
        columnDefs: [
            {
                "defaultContent": "",
                "targets": "_all"
            },
            {
                targets: 0,
                data: 'id',
                defaultContent: '',
                orderable: false,
                className: 'select-checkbox',
            },

        ],
        select: {
            style: 'multi',
            selector: 'td:first-child',
        },
        order: [[1, 'asc']],
        buttons: ButtonsArr.concat([
            'pageLength',
            {
                extend: 'excel',
                text: 'استخراج كملف إكسل',
                exportOptions: {
                    columns: ':visible:not(.notExportCol)'
                }
            },

        ]),
        select: {
            style: 'multi',
            selector: 'td:first-child'
        },
        orderCellsTop: true,
        fixedHeader: true,
        orderCellsTop: true,
        "bLengthChange": true,
        initComplete: function () {
            var api = this.api();
            // For each column
            api
                .columns(':visible')
                .eq(0)
                .each(function (colIdx) {
                    //Except last column that have Actions
                    /*  alert(this.columns(':visible').count());  */
                    /* alert(colIdx);*/
                    if (colIdx == this.columns(':visible').count() || colIdx == 0) {
                        var cell = $('.filters th').eq(
                            $(api.column(colIdx).header()).index()
                        );
                        var title = $(cell).text();
                        $(cell).html('');
                        return;
                    }
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input  type="text" style="width: ' + cell.width() + 'PX; max-width:150px"   />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('change', function (e) {
                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();
                        })
                        .on('keyup', function (e) {
                            e.stopPropagation();

                            $(this).trigger('change');
                            $(this)
                                .focus()[0]
                            /*.setSelectionRange(cursorPosition, cursorPosition);*/
                        });
                });
        },
        footerCallback: footerCallback
    });
    return table;
}

function ReLoadDataTable(tableId) {
    $('#' + tableId).DataTable().ajax.reload(null, false);
}

function ReLoadDataTableWithSearchParam(tableId, url) {
   /* alert(tableId + " " + url) */
    $('#' + tableId).DataTable().ajax.url(url).load(null, false);
}



/************************************************** Enums *********************************************************************/

const AjaxResponseStatusEnum = {
    Success: 'success',
    Failure: 'failure',
}

const AjaxActionNameArEnum = {
    Add: 'الإضافة',
    Update: 'التعديل',
}

const AjaxActionNameEnEnum = {
    Add: 'Add',
    Update: 'Update',
}

/****************************************** Toastr ********************************************************************************************/


// Set the options that I want
toastr.options = {
    "closeButton": true,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}


/******************************************************* Form ******************************************************************************************/

function InvalidMsg(textbox) {
    if (textbox.value == '') {
        textbox.setCustomValidity('يجب إدخال قيمة');
    }
    else if (textbox.validity.typeMismatch) {
        textbox.setCustomValidity('القيمة المدخلة ليست إميل');
    }
    else {
        textbox.setCustomValidity('');
    }
    return true;
}


function PostForm(FormId, PostURL) {
    if ($('#' + FormId).valid()) {
        var ProcessName = document.getElementById("Id").value == 0 ? AjaxActionNameArEnum.Add : document.getElementById("Id").value > 0 ? AjaxActionNameArEnum.Update : ""
        var ReloadActionURL = document.getElementById("ReloadActionURL").value;
        var IsUsedExternal = document.getElementById("IsUsedExternal").value;
        var ControlId = document.getElementById("ControlId").value;

        var form = document.getElementById(FormId);
        var formData_ForPost = new FormData(form);
        $.ajax({
            type: "POST",
            url: PostURL,
            processData: false,
            contentType: false,
            data: formData_ForPost,
            success: function (postFormResponse) {
                if (postFormResponse.status == AjaxResponseStatusEnum.Success) {
                    HideSpinner();
                    toastr.success("تم " + ProcessName + " بنجاح");
                    reloadAfterPostSuccess(IsUsedExternal, FormId, ControlId, ReloadActionURL, postFormResponse.dataObj)
                    if (document.getElementById("ActionType").value == AjaxActionNameEnEnum.Add) { document.getElementById(FormId).reset(); }
                    if (document.getElementById("ActionType").value == AjaxActionNameEnEnum.Update) { $('.modal').modal('hide'); }
                } else {
                    toastr.error("فشل " + ProcessName + " :" + postFormResponse.errorMessage);
                }
                console.log(postFormResponse);
            },
            error: function (error) {
                console.log(error);
                toastr.error("حدث خطأ أثناء إرسال البيانات: " + postFormResponse.errorMessage);
            }

        });
    };
}

function setModelAddUpdate(FormId, Id, ReloadActionURL, IsUsedExternal, ControlId, EntityName) {
    if (Id > 0) {
        document.getElementById(FormId).reset();
        document.getElementById("modalTitle").innerHTML = "تعديل" + " " + EntityName;
        document.getElementById("ActionType").value = "Update";
        document.getElementById("ReloadActionURL").value = ReloadActionURL;
        document.getElementById("IsUsedExternal").value = IsUsedExternal;
        document.getElementById("ControlId").value = ControlId;
    }
    else {
        document.getElementById(FormId).reset();
        document.getElementById("modalTitle").innerHTML = "إضافة" + " " + EntityName;
        document.getElementById("ActionType").value = "Add";
        document.getElementById("Id").value = "0";
        document.getElementById("ReloadActionURL").value = ReloadActionURL;
        document.getElementById("IsUsedExternal").value = IsUsedExternal;
        document.getElementById("ControlId").value = ControlId;
    }
}



function setModelDelete(ActionURL, Id, ReloadActionURL) {
    document.getElementById("DeletedActionURL").value = ActionURL;
    document.getElementById("DeletedId").value = Id;
    document.getElementById("ReloadDeletedURL").value = ReloadActionURL;
}

function reloadAfterPostSuccess(IsUsedExternal, FormId, ControlId, ReloadActionURL, PostElementId) {
    debugger
    if (IsUsedExternal === 'true') {
        PopulateDDL(ControlId, ReloadActionURL, PostElementId);
    } else {
        ReLoadDataTableWithSearchParam(ControlId, ReloadActionURL)
    }
}

const IsEmpty = str => !str.trim().length;

function OpenInTab(URL) {
    window.open(URL, '_blank');
}

/*********************************************************************************************************************************************************/


/********************************************************** Drop Down List *******************************************************************************/

function PopulateDDL(ddl_Id, url,selected) {
    var ddl_Id = $('#' + ddl_Id); // cache it
    $.getJSON(url, function (populateDDLResponse) {
        ddl_Id.empty(); // remove any existing options
        ddl_Id.append($('<option></option>').text('اختر').val(null));
        $.each(populateDDLResponse, function (index, item) {
            ddl_Id.append($('<option></option>').text(item.name).val(item.id));
        });
        ddl_Id.val(selected)
    });
}

function PopulateDDLFromList(ddl_Id, List) {
    var ddl_Id = $('#' + ddl_Id); // cache it
    ddl_Id.empty(); // remove any existing options
    ddl_Id.append($('<option></option>').text('اختر').val(-1));
    for (var item in List) {
        if (List.hasOwnProperty(item)) {
            ddl_Id.append($('<option></option>').text(List[item]).val(item));
        }
    }

}

function EmptyDDL(ddl_Id) {
    var ddl_Id = $('#' + ddl_Id); // cache it
    ddl_Id.empty(); // remove any existing options
}

/*********************************************************************************************************************************************************/


/*************************************************** Text Box Date******************************************************************************************/

function InitialTextBoxDate() {
    $('.txtDate').daterangepicker({
        locale: { format: 'YYYY-MM-DD' },
        singleDatePicker: true,
        showDropdowns: true,
        autoUpdateInput: true,
        pickDate: true,
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10),
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });

}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

/***********************************************************************************************************************************************************/


/************* Spinner *********************/
function ShowSpinner() {
    document.getElementById("overlayDiv").style.display = "block";
}

function HideSpinner() {
    document.getElementById("overlayDiv").style.display = "none";
}
/***********************************/



