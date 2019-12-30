using ConsoleApp.Dominio;

namespace ConsoleApp.DominioTest._builders
{
    public class VeiculoBuilder
    {
        private int _velocidade;
        private int _marcha;
        private string _tipo;

        public static VeiculoBuilder Novo()
        {
            return new VeiculoBuilder();
        }

        public VeiculoBuilder ComVelocidade(int velocidade)
        {
            _velocidade = velocidade;
            return this;
        }

        public VeiculoBuilder ComMarcha(int marcha)
        {
            _marcha = marcha;
            return this;
        }

        public VeiculoBuilder ComVelocidadeMarcha(int velocidade, int marcha, string tipo)
        {
            _marcha = marcha;
            _tipo = tipo;
            _velocidade = velocidade;
            return this;
        }


        public dynamic Build()
        {
            if(_tipo == "Carro")
                return new Carro(_velocidade, _marcha, _tipo);
            else
                return new Motocicleta(_velocidade, _marcha, _tipo);
        }
    }
}
