
$(document).ready(function () {

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CPF": $(this).find("#CPF").val().replace(/\D/g, ''),
                "DtNasc": $(this).find("#DtNasc").val(),
                "ListaEnderecos": recuperarLista()  
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                }
        });

    })

})

function recuperarLista() {

    return data = JSON.parse(localStorage.getItem('ListaEnderecos'))
}

//function ModalDialog(titulo, texto) {
//    var random = Math.random().toString().replace('.', '');
//    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
//        '        <div class="modal-dialog">                                                                                 ' +
//        '            <div class="modal-content">                                                                            ' +
//        '                <div class="modal-header">                                                                         ' +
//        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
//        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
//        '                </div>                                                                                             ' +
//        '                <div class="modal-body">                                                                           ' +
//        '                    <p>' + texto + '</p>                                                                           ' +
//        '                </div>                                                                                             ' +
//        '                <div class="modal-footer">                                                                         ' +
//        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
//        '                                                                                                                   ' +
//        '                </div>                                                                                             ' +
//        '            </div><!-- /.modal-content -->                                                                         ' +
//        '  </div><!-- /.modal-dialog -->                                                                                    ' +
//        '</div> <!-- /.modal -->                                                                                        ';

//    $('body').append(texto);
//    $('#' + random).modal('show');


//}

//function CadastrarBeneficiario() {

//    var CPFBenef = document.getElementById("CPFBeneficiario").value;
//    var NomeBenef = document.getElementById("NomeBeneficiario").value;
//    var Id = urlPost.replace(/^\D+/g, '');
//    var obj = {};



//    obj["CPFBeneficiario"] = CPFBenef;
//    obj["NomeBeneficiario"] = NomeBenef;
//    obj["IdCliente"] = Id;

//    $.ajax({

//        type: "POST",
//        url: "../IncluirBeneficiario",
//        data: JSON.stringify(obj),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        error:
//            function (r) {
//                if (r.status == 400)
//                    ModalDialog("Ocorreu um erro", r.responseJSON);
//                else if (r.status == 500)
//                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
//            },
//        success:
//            function (r) {
//                ModalDialog("Beneficiário cadastrado com sucesso!", r)
//                MontarGridBeneficiario(r.Records.Data);


//            }

//    })

//function is_cpf(c) {

//    if ((c = c.replace(/[^\d]/g, "")).length != 11)
//        return false

//    if (c == "00000000000")
//        return false;

//    var r;
//    var s = 0;

//    for (i = 1; i <= 9; i++)
//        s = s + parseInt(c[i - 1]) * (11 - i);

//    r = (s * 10) % 11;

//    if ((r == 10) || (r == 11))
//        r = 0;

//    if (r != parseInt(c[9]))
//        return false;

//    s = 0;

//    for (i = 1; i <= 10; i++)
//        s = s + parseInt(c[i - 1]) * (12 - i);

//    r = (s * 10) % 11;

//    if ((r == 10) || (r == 11))
//        r = 0;

//    if (r != parseInt(c[10]))
//        return false;
//    return true;
//}


//function fMasc(objeto, mascara) {
//    obj = objeto
//    masc = mascara
//    setTimeout("fMascEx()", 1)
//}

//function fMascEx() {
//    obj.value = masc(obj.value)
//}

//function mCPF(cpf) {
//    cpf = cpf.replace(/\D/g, "")
//    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
//    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
//    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2")
//    return cpf

//}


//function cpfCheck(el) {
//        document.getElementById('cpfResponse').innerHTML = is_cpf(el.value) ? document.getElementById("btnCadastrar").disabled = false : document.getElementById("btnCadastrar").disabled = true;
//        if (el.value == '') document.getElementById('cpfResponse').innerHTML = '';
//}



//function CPFIncluido(CPF) {
//    var table = document.getElementById('gridBeneficiarios');
//    if (table.rows.length > 0) {
//        contLinhas = table.rows.length;
//        for (var i = 1; i < contLinhas; i++) {
//            var CPFTabela = document.getElementById('gridBeneficiarios').rows[i].cells[0].innerText;
//            if (CPFTabela == CPF) {
//                return false;
//            }
//        }

//    }
//    return true;
}