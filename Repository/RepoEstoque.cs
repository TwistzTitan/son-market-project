using System;
using System.Collections.Generic;
using System.Linq;
using market.Data;
using market.Domain.Entity;
using market.Domain.Repository.Models;
using market.Domain.Repository.Common;
using market.Domain.Repository;
using Microsoft.EntityFrameworkCore;
public class RepoEstoque : IGestorRepoEstoque {

    private readonly ApplicationDbContext _context;

    public RepoEstoque(ApplicationDbContext context){
        _context = context;
    }

    public  RespSimplesBase<Estoque> Salvar(Estoque estq)
    {
        try{
            _context.Estoques.Add(estq);
            _context.SaveChanges();
            return RespSalvarEstoque.Sucesso(estq);
        }
        catch{
            return RespSalvarEstoque.Erro();
        }
    }

    public  RespDadosBase<Estoque> Obter(int i)
    {
        try {
            
            Estoque estoque = _context.Estoques.Single( e => e.Id == i);
            return new RespRepoEstoque(estoque);
        }
        catch {
           return new RespRepoEstoque(new Estoque(), RepoStatus.Erro);
        }
        

    }

    public RespDadosBase<Estoque> ObterEstoquesDisponiveis(){

      try {
       var estoques = _context.Estoques
                .Include(e => e.Produto)
                .Where( e => e.Quantidade > 0).ToList();
                
        return new RespRepoEstoque(estoques);
        
      }
      catch {
        return new RespRepoEstoque();
      }

    }


}