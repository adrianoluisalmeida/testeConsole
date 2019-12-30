using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp.Dominio;
using static ConsoleApp.Dominio.ValidadorDeRegra;

namespace ConsoleApp1
{
    class Program
    {

        public static int tipo;
        public static int valorMenu = 0;
        public static bool fechar = false;

        /**
        * Menu com opções para manipular a motocicleta
       */
        public static void Menu()
        {
            Console.WriteLine("\n\n === Veículo (" + (tipo == 1 ? " Motocicleta " : " Carro ") + ") === ");
            Console.WriteLine("1 - Acelerar");
            Console.WriteLine("2 - Frear");
            Console.WriteLine("3 - Aumenta Marcha");
            Console.WriteLine("4 - Reduz Marcha");
            if(tipo == 2)
                Console.WriteLine("5 - Marcha Ré");
            Console.WriteLine("0 - Fechar o programa\n");
            Console.WriteLine("\n Informe o valor que deseja acessar e confirme pressionando ENTER: ");

        }

        public static bool CapturaValorDigitado()
        {
            int valor = Convert.ToInt32(Console.ReadLine());

            if (valor >= 0 && valor < (tipo == 1 ? 5 : 6))
            {
                valorMenu = valor;
                return true;
            }
            else
            {
                Console.WriteLine("Valor inválido");
                return false;
            }
        }

        static void Main(string[] args)
        {
            Veiculo veiculo;

            Console.WriteLine("\n\n === Selecionar veículo === ");
            Console.WriteLine("1 - Motocicleta");
            Console.WriteLine("2 - Carro");
            Console.WriteLine("\n Informe o valor que deseja acessar e confirme pressionando ENTER: ");

            int valor = Convert.ToInt32(Console.ReadLine());
            tipo = valor; // 1 Motocicleta - 2 Carro

            if (valor == 1)
                veiculo = new Motocicleta(0, 0, "Motocicleta");
            else
                veiculo = new Carro(0, 0, "Carro");

            Console.Write("\nVeículo atual:" + veiculo.Tipo);
            Console.Write("\nVelocidade atual:" + veiculo.Velocidade);
            Console.Write("\nMarcha atual:" + RetornaMarcha(veiculo.Marcha));

            while (!fechar)
            {

                Menu();
                if (CapturaValorDigitado())
                {
                    try
                    {
                        switch (valorMenu)
                        {
                            case 0:
                                fechar = true;
                                break;
                            case 1:
                                veiculo.acelera();
                                break;
                            case 2:
                                veiculo.freia();
                                break;
                            case 3:
                                veiculo.aumentaMarcha();
                                break;
                            case 4:
                                veiculo.reduzMarcha();
                                break;
                            case 5:
                                veiculo.MarchaRe();
                                break;
                            

                        };

                        Console.Write("\nVelocidade atual:" + veiculo.Velocidade);
                        Console.Write("\nMarcha atual:" + RetornaMarcha(veiculo.Marcha));
                    }
                    catch (ExcecaoDeDominio exception)
                    {
                        foreach (string mensagem in exception.MensagensDeErro)
                            Console.Write(mensagem);

                    }

                }
            }

            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);
       

        
        }

        public static string RetornaMarcha(int _marcha)
        {
            string _stringMarcha = "";

            switch (_marcha)
            {
                case 0:
                    _stringMarcha = "Neutro";
                    break;
                case 1:
                    _stringMarcha = "Primeira";
                    break;
                case 2:
                    _stringMarcha = "Segunda";
                    break;
                case 3:
                    _stringMarcha = "Terceira";
                    break;
                case 4:
                    _stringMarcha = "Quarta";
                    break;
                case 5:
                    _stringMarcha = "Quinta";
                    break;
                case 6:
                    _stringMarcha = "Ré";
                    break;

            }

            return _stringMarcha;
        }
    }
}