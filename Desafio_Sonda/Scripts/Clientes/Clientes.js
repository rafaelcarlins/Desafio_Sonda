
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

function MontarGridClientes(Lista) {

    var d = document;

    var newRow = d.createElement('tr');
    var contLinhas;
    var table = d.getElementById('gridClientes');
    if (table.rows.length > 0) {
        contLinhas = table.rows.length;
        for (var i = 0; i < contLinhas; i++) {
            d.getElementById('gridClientes').deleteRow(0);
        }
    }
    newCol = d.createElement('tr');
    newCol.insertCell(0).innerHTML = "Nome";
    newCol.insertCell(1).innerHTML = "CPF";
    newCol.insertCell(2).innerHTML = "Data Nascimento";


    d.getElementById('gridClientes').appendChild(newCol);
    for (var i = 0; i < Lista.Records.length; i++) {
        var newBtnAlterar = d.createElement('Button');
        newBtnAlterar.setAttribute("class", "btn btn-link");
        newBtnAlterar.onclick = function (e) {
            e = e || window.event;
            var target = e.target || e.srcElement;
            if (target.innerHTML == "Alterar") {
                AlterarCliente(target.parentNode.rowIndex);
                e.preventDefault();
            }
            else {
                e.preventDefault();
            }
        };
        var newBtnDeletar = d.createElement('Button');
        newBtnDeletar.setAttribute("class", "btn btn-danger delete");
        newBtnDeletar.onclick = function (e) {
            e = e || window.event;
            var target = e.target || e.srcElement;
            if (target.innerHTML == "Deletar") {
                if (confirm('Deseja realmente excluir?')) {
                    ExcluirCliente(target.parentNode.rowIndex);
                    e.preventDefault();
                }
                else {
                    e.preventDefault();
                }
            }
        };
        newRow.insertCell(0).innerHTML = Lista.Records[i].Nome;
        newRow.insertCell(1).innerHTML = Lista.Records[i].CPF;
        newRow.insertCell(2).innerHTML = Lista.Records[i].DtNasc;
        newBtnAlterar.innerHTML = "Alterar";
        newBtnDeletar.innerHTML = "Deletar";

        newRow.appendChild(newBtnAlterar);
        newRow.appendChild(newBtnDeletar);
        d.getElementById('gridClientes').appendChild(newRow);
        newRow = d.createElement('tr');
        newCol = d.createElement('tr');
        newBtnAlterar = d.createElement('Button');
        newBtnDeletar = d.createElement('Button');
        newBtnAlterar.setAttribute("class", "btn btn-link");
        newBtnDeletar.setAttribute("class", "btn btn-danger delete");
    }

    salvarLista(Lista.Records);

}

function salvarLista(Lista) {
    try {
        if (Lista.length > 0) {
            for (var i = 0; i < Lista.length; i++) {
                localStorage.setItem('ListaBeneficiario', JSON.stringify(Lista))
            }
        }
        else {
            localStorage.clear();
        }

    } catch (e) {

    }
}

function recuperarLista() {

    return data = JSON.parse(localStorage.getItem('ListaEnderecos'))
}
