using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Dominio
{
    public class Veiculo
    {

        public bool ReducaoPermitida = true;

        public string Tipo { get; set; }
        public int Velocidade { get; private set; }
        public int Marcha { get; private set; }


        public Veiculo(int velocidade, int marcha, string tipo)
        {

            ValidadorDeRegra.Novo()
                .Quando(velocidade < 0, "Velocidade inválida")
                .Quando(marcha < 0, "Marcha inválida")
                .Quando(marcha > 5, "Marcha inválida")
                .DispararExcecaoSeExistir();

            Velocidade = velocidade;
            Marcha = marcha;
            Tipo = tipo;
        }

        public void setMarcha(int marcha)
        {
            Marcha = marcha;
        }


        public void setReducaoPermitida(bool permissao)
        {
            ReducaoPermitida = permissao;
        }

        //Retorna a velocidade Máxima
        public int getVelocidadeMax(bool marcha_anterior = false)
        {

            return marcha_anterior ? (Marcha - 1) * 20 : Marcha * 20;
        }

        //Retorna a velocidade Mínima
        public int getVelocidadeMin()
        {
            return getVelocidadeMax() - 20;
        }

        public bool getMarchaSuperiorDaVelocidadeAtual()
        {
            if (Velocidade < getVelocidadeMin())
                return true;
            else
                return false;
        }

        //Incrementa a velocidade em 5
        public void acelera()
        {
            if (Velocidade == 0)
            {
                if (Marcha == 1 || Marcha == 6)
                {
                    Velocidade += 5;

                    //Testa se com a nova velocidade poderá reduzir a marcha novamente
                    if (TestaVelocidadeMarchaAnterior())
                        setReducaoPermitida(true);
                }


                ValidadorDeRegra.Novo()
                    .Quando(Marcha != 1 && Marcha != 6, "A Velocidade está em 0, a marcha precisa estar em 1.")
                    .DispararExcecaoSeExistir();
            }
            else
            {
                ValidadorDeRegra.Novo()
                    .Quando(Velocidade >= getVelocidadeMax(), "A velocidade máxima da marcha atual foi atingida, aumente a marcha para acelerar.")
                    .DispararExcecaoSeExistir();

                if (Velocidade <= getVelocidadeMax())
                    if (getMarchaSuperiorDaVelocidadeAtual())
                        freia(2);
                    else
                        Velocidade += 5;

                //Testa se com a nova velocidade poderá reduzir a marcha novamente
                if (TestaVelocidadeMarchaAnterior())
                    setReducaoPermitida(true);
            }

        }

        //Decrementa a velocidade em -8 ou de acordo com o valor passado por param
        public void freia(int velocidade = 8)
        {
            ValidadorDeRegra.Novo()
                 .Quando(Velocidade == 0, "A velocidade já está em 0, não é possível reduzir mais.")
                 .DispararExcecaoSeExistir();

            if (Velocidade > 0)
                if (Velocidade - velocidade < 0)
                    Velocidade = 0;
                else
                    Velocidade -= velocidade;
          

        }

        //Aumenta a Marcha em 1
        public void aumentaMarcha()
        {
            ValidadorDeRegra.Novo()
                   .Quando(Marcha > 5, "A marcha máxima foi atiginda.")
                   .DispararExcecaoSeExistir();

            if (Marcha < 5)
                Marcha += 1;
            //Testa se com a nova marcha poderá reduzir a marcha novamente
            if (TestaVelocidadeMarchaAnterior())
                setReducaoPermitida(true);

        }

        //Reduz a Marcha em 1
        public void reduzMarcha()
        {
            if (Marcha - 1 == 0 && Velocidade == 0)
                setReducaoPermitida(true);

            ValidadorDeRegra.Novo()
               .Quando(ReducaoPermitida == false, "Não é possivel mais reduzir a marcha.")
               .Quando(Marcha - 1 < 0 && Velocidade != 0, "Não é possivel mais reduzir a marcha.")
               .DispararExcecaoSeExistir();

            
            if (Velocidade > getVelocidadeMax(true))
            {
                if (ReducaoPermitida && Marcha - 1 > 0)
                    Marcha -= 1;

                setReducaoPermitida(false);
            }
            else
            {
                if (Marcha - 1 > 0)
                    Marcha -= 1;
                else
                    if (Velocidade == 0)
                        Marcha = 0;

            }

        }

        private bool TestaVelocidadeMarchaAnterior()
        {
            if (Velocidade > getVelocidadeMax(true))
            {
                return true;
            }

            return false;
        }

        //Escopo do method marchare, implementação na classe carro
        public virtual void MarchaRe() { }






    }


}
