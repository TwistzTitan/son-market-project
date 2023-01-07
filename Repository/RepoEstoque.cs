using System;
using System.Collections.Generic;
using System.Linq;
using market.Data;
using market.Domain.Entity;
using market.Domain.Repository.Models;
using market.Domain.Repository.Common;
using market.Domain.Repository;
using Microsoft.EntityFrameworkCore;
public class RepoEstoque : IRepoEstoque,IGestorRepoEstoque {

    private readonly ApplicationDbContext _context;

    public RepoEstoque(ApplicationDbContext context){
        _context = context;
    }

    public override RespSalvarEstoque Salvar(Estoque estq)
    {
        try{
            _context.Estoques.Add(estq);
            _context.SaveChanges();
            return RespSalvarEstoque.Sucesso();
        }
        catch{
            return RespSalvarEstoque.Erro();
        }
    }

    public override RespObterEstoque Obter(int i)
    {
        try {
            
            Estoque estoque = _context.Estoques.Single( e => e.Id == i);
            return new RespObterEstoque(estoque);
        }
        catch {
           return new RespObterEstoque(new Estoque(), RepoStatus.Erro);
        }
        

    }

    public IList<Estoque> ObterEstoquesDisponiveis(){

       return _context.Estoques
                .Include(e => e.Produto)
                .Where( e => e.Quantidade > 0).ToList();

    }


}