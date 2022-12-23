
const PRODUTO_ADDR = "https://localhost:7166/Produtos/"
const BUSCAR_PRODUTO_ACTION = "Buscar";

class Produto {
    
    id;
    nome; 
    categoria; 
    fornecedor;
    medicao;
    medicaoNome; 
    preco;

    static CriarProduto(p){

        let novoProduto = new Produto();
        novoProduto.id = p.id;
        novoProduto.nome = p.nome;
        novoProduto.categoria = p.categoriaNome;
        novoProduto.fornecedor = p.fornecedorNome;
        novoProduto.medicao = p.medicao;
        novoProduto.preco = p.precoVenda;
        novoProduto.medicaoNome = Produto.MedicaoParse(novoProduto.medicao);
        
        return novoProduto;
    }
    
    static MedicaoParse(m){
        switch(m){
            case 0:
                return "L";
            case 1: 
                return "KG";
            case 2: 
                return "UN";
            default:
                return "UN";
        }
    }
}

class Item {
    static itemId = 0;
    id;
    produtoId;
    produtoNome;
    quantidade;
    preco;
    medida;
    total;
    
    static criarItem(prod,qtd){
        let item = new Item();
        Item.itemId +=1;
        item.id = Item.itemId; 
        item.produtoId = prod.id;
        item.produtoNome = prod.nome;
        item.qtd = qtd;
        item.preco = prod.preco;
        item.medida = prod.medicaoNome;
        item.total = qtd * item.preco;
        
        return item;
    }
}

itemHTML = (i) => `
    <tr id="item_${i.id}}">
        <td>${i.produtoId}</td>
        <td>${i.produtoNome}</td>
        <td>${i.qtd}</td>
        <td>${i.preco}</td>
        <td>${i.medida}</td>
        <td>${i.total}</td>
        <td><button class="btn btn-danger" onclick="removerItem(${i.id})">Remover</button></td>
    <tr>`;


adicionarItemTabela = (i) => $('#tb_Itens').append(itemHTML(i))

removerItem = (id) => {
    if(confirm("Deseja remover o item da compra?")){
        var filteredItens =  itensDeVenda.filter( i => i.id !== id);
        itensDeVenda = filteredItens;
        var item = document.getElementById(`item_${id}}`)
        item.remove();
    }
}

// Produto pré-venda

let produtoPreVenda = new Produto();

// Produtos confirmados

let itensDeVenda = [];

// Declaração de eventos

$('#btn_Busca').click(e => buscarProduto($('#cod_Busca').val()));
$('#btn_Confirma').click(e => { e.preventDefault(); confirmaProduto($('#qtdProd').val())});

//Funções


function buscarProduto (id){
    let POST_COMMAND = PRODUTO_ADDR + BUSCAR_PRODUTO_ACTION;
    $.post(
        POST_COMMAND,
        {id}, 
        (data) => produtoEncontrado(data)
    )
    .fail((xhr,status,error)=>{
        let response = JSON.parse(xhr.responseText);
        alert(response);
        limparInformacoes();
    })
}

function produtoEncontrado(p){
    
    let produto = Produto.CriarProduto(p);

    Object.assign(produtoPreVenda,produto);

    preencherInformacoes(produto);

}

function limparInformacoes(){

    $('#nomeProd').val('');
    $('#catProd').val('');
    $('#fornProd').val('');
    $('#pvProd').val('');
    $('#cod_Busca').val('');
    $('#qtdProd').val('');
}

function preencherInformacoes(p){

    $('#nomeProd').val(p.nome);
    $('#catProd').val(p.categoria);
    $('#fornProd').val(p.fornecedor);
    $('#pvProd').val(p.preco);
    $('#qtdProd').removeAttr('readonly');
    $('#btn_Confirma').removeAttr('disabled');

}

function confirmaProduto(q){
    if(produtoPreVenda.id === undefined){
        alert('Buscar novo produto');
        return false;
    }

    let produto = new Produto();
    Object.assign(produto,produtoPreVenda);
    limparInformacoes()
    produtoPreVenda = new Produto();
    let item = Item.criarItem(produto,q);
    itensDeVenda.push(item);
    adicionarItemTabela(item);
    $('#btn_Confirma').attr('disabled','true');
    $('#qtdProd').attr('readonly','true');
    return false;
}
