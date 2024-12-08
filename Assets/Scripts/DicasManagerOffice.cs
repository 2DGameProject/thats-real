using TMPro;  // Para utilizar TextMeshPro se estiver usando esse componente
using UnityEngine;
using UnityEngine.UI;

public class DicasManagerOffice : MonoBehaviour
{
    public static DicasManagerOffice Instance { get; private set; }  // Inst�ncia est�tica para Singleton

    public Button botaoMostrarOpcoes;    // Bot�o principal para mostrar o menu de op��es
    public GameObject painelOpcoes;      // Painel que cont�m os bot�es de Dica e An�ncio
    public Button botaoDica;             // Bot�o para ver a primeira dica
    public Button botaoAnuncio;          // Bot�o para ver o v�deo de an�ncio
    public Button botaoFechar;           // Bot�o para fechar o painel de op��es
    public TextMeshProUGUI dicaTexto;    // Texto para exibir a dica
    private bool dicaVisto = false;      // Flag para saber se a dica foi vista
    private bool primeiraDica = true;    // Flag para controlar se � a primeira vez que o jogador est� vendo a dica
    private bool botaoDicaVisivel = true; // Flag para controlar a visibilidade do bot�o de Dica
    private bool botaoAnuncioVisivel = false; // Flag para controlar a visibilidade do bot�o de An�ncio

    void Awake()
    {
        // Garante que s� exista uma inst�ncia do DicasManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destr�i o objeto se uma inst�ncia j� existir
            return;
        }

        // N�o destrua o DicasManager entre cenas
    }

    void Start()
    {
        // Inicialmente o painel de op��es est� desativado
        painelOpcoes.SetActive(false);

        // Configura os bot�es
        botaoMostrarOpcoes.onClick.AddListener(MostrarOpcoes);
        botaoDica.onClick.AddListener(ExibirDica);
        botaoAnuncio.gameObject.SetActive(false);  // Inicialmente desativa o bot�o de an�ncio
        botaoFechar.onClick.AddListener(CerrarMenu);
    }

    // Fun��o para mostrar o painel de op��es
    void MostrarOpcoes()
    {
        painelOpcoes.SetActive(true);  // Ativa o painel de op��es

        // Exibe os bot�es com base nas flags
        botaoDica.gameObject.SetActive(botaoDicaVisivel);
        botaoAnuncio.gameObject.SetActive(botaoAnuncioVisivel);
    }

    // Fun��o para exibir a dica gratuita
    void ExibirDica()
    {
        // Exibe a dica inicial
        dicaTexto.text = "Dica gr�tis: Preste aten��o no computador do Pedro.\n\n" +
                         "Voc� pode assistir um an�ncio para ganhar outra dica.";

        // Marca que a dica foi vista
        dicaVisto = true;
        primeiraDica = false;

        // Esconde o bot�o de Dica
        botaoDicaVisivel = false; // Marca o bot�o de Dica como invis�vel
        botaoAnuncioVisivel = true; // Marca o bot�o de An�ncio como vis�vel

        // Esconde o bot�o de Dica
        botaoDica.gameObject.SetActive(botaoDicaVisivel);

        // Mostra o bot�o de An�ncio (ap�s ver a dica)
        botaoAnuncio.gameObject.SetActive(botaoAnuncioVisivel);
    }

    // Fun��o chamada ap�s assistir o an�ncio para atualizar a dica
    public void AtualizarDica()
    {
        dicaTexto.text = "Dica gr�tis: Preste aten��o no computador do Pedro.\n\n" +
                         "Dica: O �ltimo usu�rio foi�.\n\n";

        // Esconde o bot�o de An�ncio ap�s o an�ncio ser assistido
        botaoAnuncio.gameObject.SetActive(false);
        botaoAnuncioVisivel = false;
    }

    // Fun��o para fechar o menu de op��es
    void CerrarMenu()
    {
        painelOpcoes.SetActive(false);  // Desativa o painel de op��es
    }
}
