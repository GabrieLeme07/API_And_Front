using FluentValidation;
using System;

namespace API.Models.Validation
{
    public class ExcelDataValidation : AbstractValidator<ExcelData>
    {
        public ExcelDataValidation()
        {
            //TODOS OS CAMPOS OBRIGATORIOS

            //O CAMPO DATA DE ENTREGA NÃO PODE SER MENOR OU IGUAL QUE O DIA ATUAL
            RuleFor(e => e.DataEntrega)
                    .NotEmpty()
                    .WithMessage("A data é obrigatório.")
                    .GreaterThan(DateTime.Today)
                    .WithMessage("a data de entrega não pode ser menor ou igual ao dia atual.");

            //O CAMPO DESCRIÇÃO PRECISA TER O TAMANHO MÁXIMO DE 50 CARACTERES
            RuleFor(e => e.DescricaoProduto)
                    .NotEmpty()
                    .WithMessage("O nome do produto é obrigatório.")
                    .MaximumLength(50)
                    .WithMessage("O nome do produto deve ter no máximo {MaxLength} caracteres.");

            //O CAMPO QUANTIDADE TEM QUE SER MAIOR DO QUE 0
            RuleFor(e => e.QtdProduto)
                    .NotEmpty()
                    .WithMessage("A quantidade é obrigatório.")
                    .GreaterThan(0)
                    .WithMessage("A quantidade tem que ser maior que {ComparisonValue}.");
            
            //O CAMPO VALOR UNITARIO DEVE SER MAIOR QUE ZERO
            RuleFor(e => e.ValorUnitario)
                    .NotEmpty()
                    .WithMessage("O valor unitário é obrigatório.")
                    .GreaterThan(0)
                    .WithMessage("O valor unitário tem que ser maior que {ComparisonValue}.");
        }
    }
}
