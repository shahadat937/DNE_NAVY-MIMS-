$(function (e) {
  $('#example').DataTable();
  $('#shipDataTable').DataTable();
  $('#shipDataTable1').DataTable();
  $('#shipDataTable2').DataTable();
  $('#shipDataTable3').DataTable();
  $('#shipDataTable4').DataTable();
  $('#shipDataTable5').DataTable();
  $('#shipDataTable6').DataTable();
  $('#shipDataTable7').DataTable();

  var table = $('#example1').DataTable();
  $('#example2').DataTable({
    "scrollY": "200px",
    "scrollCollapse": true,
    "paging": false
  });
  $('#example3').DataTable({
    responsive: {
      details: {
        display: $.fn.dataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            return 'Message Details';
          }
        }),
        renderer: $.fn.dataTable.Responsive.renderer.tableAll({
          tableClass: 'table'
        })
      }
    }
  });
});