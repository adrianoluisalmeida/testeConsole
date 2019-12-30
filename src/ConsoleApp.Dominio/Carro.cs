using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Dominio
{
    public class Carro : Veiculo
    {
        public Carro(int velocidade, int marcha, string tipo) : base(velocidade, marcha, tipo)
        { }

        public override void MarchaRe()
        {

            ValidadorDeRegra.Novo()
              .Quando(Velocidade > 0, "Para acionar a ré, primeiro pare o carro.")
               .DispararExcecaoSeExistir();

            if (Velocidade == 0)
                setReducaoPermitida(true);
                setMarcha(6);
        }
    }
}
