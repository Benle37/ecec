$(function () {
    $('.btndelete').off('click').click(function (e) {
        e.preventDefault();
        var name = $(this).data('name');
        if (confirm('Bạn muốn xoá sinh viên ' + name)) {
            var id = $(this).data('id');
            $.ajax({
                url: "/Student/Delete",
                data: JSON.stringify({ id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.status == "1") {
                        showToast(`Xoá sinh viên ${name} thành công.`);
                        $('#student_' + id).hide(200);
                        $('#student_' + id).addClass('check');
                        var check = true;
                        var child = $('.table-student tbody').children('tr');
                        child.each(function () {
                            if(!$(this).hasClass('check'))                            
                            {
                                check=false;
                                return false;
                            }
                        })
                        if (check == true) {
                            $('.table-student').hide(200);
                            $('.noti').show(400);
                            $('.btnadd').hide(200);
                        }                       
                    }
                    else
                        showToast(`Xoá sinh viên ${name} thất bại`);
                },
                error: function (data) {

                    alert(JSON.stringify(data));
                }
            })
        }
    })
})
