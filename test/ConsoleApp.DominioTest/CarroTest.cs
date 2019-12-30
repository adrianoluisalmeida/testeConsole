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
    public class CarroTest : IDisposable
    {
        private readonly ITestOutputHelper _output;

        private readonly int _velocidade;
        private readonly int _marcha;
        private readonly string _tipo;

        public Carro CarroInicial;
        public Carro Carro;
        Carro carro;

        public CarroTest(ITestOutputHelper output)
        {
            _output = output;
            _velocidade = 0;
            _marcha = 0;
            _tipo = "Carro";

            carro = VeiculoBuilder.Novo().ComVelocidadeMarcha(_velocidade, _marcha, _tipo).Build();
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }


        [Fact]
        public void IniciaCarroTest()
        {
            //Organização
            CarroInicial = new Carro(_velocidade, _marcha, _tipo);

            //Ação
            Carro carro = new Carro(CarroInicial.Velocidade, CarroInicial.Marcha, _tipo);

            //Assert
            CarroInicial.ToExpectedObject().ShouldEqual(carro);
        }

        [Theory]
        [InlineData(-1)]
        public void AcelerarCarroTest(int velocidadeInvalida)
        {
            //testa velocidade inválida
            Assert.Throws<ExcecaoDeDominio>(() => VeiculoBuilder.Novo().ComVelocidade(velocidadeInvalida).Build())
                .ComMensagem("Velocidade inválida");


            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => carro.acelera());

        }

        [Fact]
        public void FrearCarroTest()
        {

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => carro.freia())
                .ComMensagem("A velocidade já está em 0, não é possível reduzir mais.");
        }

        [Fact]
        public void ReduzirMarchaTest()
        {
            carro.reduzMarcha();
        }

        [Fact]
        public void AumentarMarchaTest()
        {
            carro.aumentaMarcha();
        }


        [Fact]
        public void MarchaReTest()
        {

            //Assert
            carro.MarchaRe();
        }

        
      
    }
}
