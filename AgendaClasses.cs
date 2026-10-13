using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaApp
{
    public class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public void SetData(int dia, int mes, int ano)
        {
            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public override string ToString()
        {
            return $"{dia:D2}/{mes:D2}/{ano}";
        }
    }

    public class Telefone
    {
        private string tipo;
        private string numero;
        private bool principal;

        public string Tipo { get => tipo; set => tipo = value; }
        public string Numero { get => numero; set => numero = value; }
        public bool Principal { get => principal; set => principal = value; }
    }

    public class Contato
    {
        private string email;
        private string nome;
        private Data dtNasc;
        private List<Telefone> telefones;

        public string Email { get => email; set => email = value; }
        public string Nome { get => nome; set => nome = value; }
        public Data DtNasc { get => dtNasc; set => dtNasc = value; }
        public List<Telefone> Telefones => telefones;

        public Contato()
        {
            telefones = new List<Telefone>();
            dtNasc = new Data();
        }

        public int GetIdade()
        {
            // Lógica simples de cálculo de idade baseada no ano atual
            // (Pode ser refinada no Windows Forms com DateTime atual)
            return DateTime.Now.Year - 2000; // Ajustado posteriormente no form
        }

        public void AdicionarTelefone(Telefone t)
        {
            if (t.Principal)
            {
                // Se o novo for principal, desmarca os outros
                foreach (var tel in telefones)
                {
                    tel.Principal = false;
                }
            }
            telefones.Add(t);
        }

        public string GetTelefonePrincipal()
        {
            var principal = telefones.FirstOrDefault(t => t.Principal);
            return principal != null ? principal.Numero : (telefones.FirstOrDefault()?.Numero ?? "Sem telefone");
        }

        public override string ToString()
        {
            return $"Nome: {nome} | E-mail: {email} | Nasc: {dtNasc} | Tel. Principal: {GetTelefonePrincipal()}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Contato outro)
            {
                return this.email.Equals(outro.email, StringComparison.OrdinalIgnoreCase) ||
                       this.nome.Equals(outro.nome, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }

    public class Contatos
    {
        private readonly List<Contato> agenda = new List<Contato>();

        public List<Contato> Agenda => agenda;

        public bool Adicionar(Contato c)
        {
            if (pesquisar(c) != null) return false; // Já existe
            agenda.Add(c);
            return true;
        }

        public Contato pesquisar(Contato c)
        {
            return agenda.FirstOrDefault(x => x.Equals(c));
        }

        public bool Alterar(Contato c)
        {
            var existente = pesquisar(c);
            if (existente != null)
            {
                existente.Email = c.Email;
                existente.DtNasc = c.DtNasc;
                return true;
            }
            return false;
        }

        public bool Remover(Contato c)
        {
            var existente = pesquisar(c);
            if (existente != null)
            {
                agenda.Remove(existente);
                return true;
            }
            return false;
        }
    }
}
