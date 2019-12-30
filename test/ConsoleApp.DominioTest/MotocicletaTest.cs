using ConsoleApp.Dominio;
using ConsoleApp.DominioTest._builders;
using ConsoleApp.DominioTest._util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;
using static ConsoleApp.Dominio.ValidadorDeRegra;

namespace ConsoleApp.DominioTest
{
    public class MotocicletaTest : IDisposable
    {
        private readonly ITestOutputHelper _output;

        private readonly int _velocidade;
        private readonly int _marcha;
        private readonly string _tipo;

        public Veiculo MotoInicial;
        public Veiculo Moto;
        Veiculo moto;

        public MotocicletaTest(ITestOutputHelper output)
        {
            _output = output;
            _velocidade = 0;
            _marcha = 0;
            _tipo = "Motocicleta";

            moto = VeiculoBuilder.Novo().ComVelocidadeMarcha(_velocidade, _marcha, _tipo).Build();
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }


        [Fact]
        public void IniciaMotocicletaTest() {

            MotoInicial = new Veiculo(_velocidade, _marcha, _tipo);

            //Ação
            Veiculo moto = new Veiculo(MotoInicial.Velocidade, MotoInicial.Marcha, _tipo);

            //Assert
            MotoInicial.ToExpectedObject().ShouldEqual(moto);
        }

        [Theory]
        [InlineData(-1)]
        public void AcelerarMotocicletaTest(int velocidadeInvalida)
        {
            //testa velocidade inválida
            Assert.Throws<ExcecaoDeDominio>(() => VeiculoBuilder.Novo().ComVelocidade(velocidadeInvalida).Build())
                .ComMensagem("Velocidade inválida");


            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => moto.acelera());
            
        }  
            
        [Fact]
        public void FrearMotocicletaTest()
        {

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => moto.freia())
                .ComMensagem("A velocidade já está em 0, não é possível reduzir mais.");
        }

        [Fact]
        public void ReduzirMarchaTest()
        {
            //Assert
            moto.reduzMarcha();
        }

        [Fact]
        public void AumentarMarchaTest()
        {
            moto.aumentaMarcha();
        }
        
    }
}
