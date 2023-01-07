// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/************************************************* DataTable *************************************************************************/
 
function AddDataTable(tableId, GetURL, columnsArr)
{

    $('#' + tableId +' thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#' + tableId +' thead');

    var table = $('#' + tableId).dataTable({
        dom: 'Bfrtip',
        "ajax": {
            url: GetURL,
            type: "GET",
            dataSrc: function (resdata) {
                
                return resdata;
            }
        },
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
                    if (colIdx == this.columns(':visible').count()  )
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

function ReLoadDataTable(tableId) {
    $('#' + tableId).DataTable().ajax.reload();
}

function ReLoadDataTableWithSearchParam(tableId,url) {
    $('#' + tableId).DataTable().ajax.url(url).load();
}

/************************************************** Enums *********************************************************************/

const AjaxResponseStatusEnum = {
    Success:'success',
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
    $('#' + FormId).submit(function (e) {
        e.preventDefault(); // avoid to execute the actual submit of the form.
        
        var ProcessName = document.getElementById("Id").value == 0 ? AjaxActionNameArEnum.Add : document.getElementById("Id").value > 0 ? AjaxActionNameArEnum.Update : ""
        var form = $(this);
        $.ajax({
            type: "POST",
            url: PostURL,
            data: form.serialize(), // serializes the form's elements.
            success: function (response) {
                if (response.status == AjaxResponseStatusEnum.Success) {
                    toastr.success("تم " + ProcessName + " بنجاح");
                    ReLoadDataTable('tbl_Items')
                    if (document.getElementById("ActionType").value == AjaxActionNameEnEnum.Add) { document.getElementById(FormId).reset(); }
                    if (document.getElementById("ActionType").value == AjaxActionNameEnEnum.Update) { $('.modal').modal('hide'); }
                } else {
                    toastr.error("فشل " + ProcessName + " :" + response.errorMessage);
                }
                console.log(response);
            },
            error: function (error) {
                console.log(error);
                toastr.error("حدث خطأ أثناء إرسال البيانات: " + response.errorMessage);
            }

        });
    });
}

function setModelAddUpdate(FormId, Id) {
    if (Id > 0) {
        document.getElementById(FormId).reset();
        document.getElementById("modalTitle").innerHTML = "تعديل";
        document.getElementById("ActionType").value = "Update";
    }
    else {
        document.getElementById(FormId).reset();
        document.getElementById("modalTitle").innerHTML = "إضافة";
        document.getElementById("ActionType").value = "Add";
        document.getElementById("Id").value = "0";
    }
}

function setModelDelete(ActionURL, Id, ReloadActionURL) {
    document.getElementById("DeletedActionURL").value = ActionURL;
    document.getElementById("DeletedId").value = Id;
    document.getElementById("ReloadActionURL").value = ReloadActionURL;
}

const IsEmpty = str => !str.trim().length;

/*********************************************************************************************************************************************************/


/********************************************************** Drop Down List *******************************************************************************/

function PopulateDDL(ddl_Id, url) {
    var ddl_Id = $('#' + ddl_Id ); // cache it
        $.getJSON(url , function (response) {
            ddl_Id.empty(); // remove any existing options
            ddl_Id.append($('<option></option>').text('اختر').val(-1));
            $.each(response, function (index, item) {
                ddl_Id.append($('<option></option>').text(item.name).val(item.id));
            });
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

/*********************************************************************************************************************************************************/


/*************************************************** Text Box Date******************************************************************************************/
function InitialTextBoxDate() {
    $('.txtDate').daterangepicker({
        locale: { format: 'YYYY-MM-DD' },
        singleDatePicker: true,
        showDropdowns: true,
        //autoUpdateInput: false,
        pickDate: false,
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
}


/***********************************************************************************************************************************************************/