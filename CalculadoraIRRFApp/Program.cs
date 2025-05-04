using System;

public class CalculadoraSalario
{
    public double SalarioBruto { get; set; }
    public int Dependentes { get; set; }

    public CalculadoraSalario(double salarioBruto, int dependentes)
    {
        SalarioBruto = salarioBruto;
        Dependentes = dependentes;
    }

    public double CalcularINSS()
    {
        double inss = 0;
        double salario = SalarioBruto;

        if (salario <= 1518.00)
        {
            inss = salario * 0.075;
        }
        else if (salario <= 2793.88)
        {
            inss = 1518.00 * 0.075;
            inss += (salario - 1518.00) * 0.09;
        }
        else if (salario <= 4190.83)
        {
            inss = 1518.00 * 0.075;
            inss += (2793.88 - 1518.00) * 0.09;
            inss += (salario - 2793.88) * 0.12;
        }
        else if (salario <= 8157.41)
        {
            inss = 1518.00 * 0.075;
            inss += (2793.88 - 1518.00) * 0.09;
            inss += (4190.83 - 2793.88) * 0.12;
            inss += (salario - 4190.83) * 0.14;
        }
        else
        {
            inss = 1518.00 * 0.075;
            inss += (2793.88 - 1518.00) * 0.09;
            inss += (4190.83 - 2793.88) * 0.12;
            inss += (8157.41 - 4190.83) * 0.14;
        }

        return Math.Round(inss, 2);
    }

    public double CalcularSalarioBase()
    {
        double inss = CalcularINSS();
        double deducaoDependentes = Dependentes * 189.59;
        return SalarioBruto - inss - deducaoDependentes;
    }

    public double CalcularIRRF()
    {
        double salarioBase = CalcularSalarioBase();
        double aliquota = 0;
        double deducao = 0;

        if (salarioBase <= 2259.20)
        {
            return 0;
        }
        else if (salarioBase <= 2826.65)
        {
            aliquota = 0.075;
            deducao = 169.44;
        }
        else if (salarioBase <= 3751.05)
        {
            aliquota = 0.15;
            deducao = 381.44;
        }
        else if (salarioBase <= 4664.68)
        {
            aliquota = 0.225;
            deducao = 662.77;
        }
        else
        {
            aliquota = 0.275;
            deducao = 896.00;
        }

        double irrf = salarioBase * aliquota - deducao;
        return Math.Round(irrf < 0 ? 0 : irrf, 2);
    }

    public double CalcularSalarioLiquido()
    {
        double inss = CalcularINSS();
        double irrf = CalcularIRRF();
        return Math.Round(SalarioBruto - inss - irrf, 2);
    }

    public void ExibirResumo()
    {
        double inss = CalcularINSS();
        double irrf = CalcularIRRF();
        double salarioLiquido = CalcularSalarioLiquido();

        Console.WriteLine("Salário Bruto: R$ " + SalarioBruto.ToString("F2"));
        Console.WriteLine("INSS: R$ " + inss.ToString("F2"));
        Console.WriteLine("IRRF: R$ " + irrf.ToString("F2"));
        Console.WriteLine("Desconto por dependentes: R$ " + (Dependentes * 189.59).ToString("F2"));
        Console.WriteLine("Salário Líquido: R$ " + salarioLiquido.ToString("F2"));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Digite o salário bruto: ");
        double salarioBruto = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Digite o número de dependentes: ");
        int dependentes = Convert.ToInt32(Console.ReadLine());

        CalculadoraSalario calculadora = new CalculadoraSalario(salarioBruto, dependentes);
        calculadora.ExibirResumo();
    }
}
