using TMPro;  // Para utilizar TextMeshPro se estiver usando esse componente
using UnityEngine;
using UnityEngine.UI;

public class DicasManagerBoss : MonoBehaviour
{
    public static DicasManagerBoss Instance { get; private set; }  // Instância estática para Singleton

    public Button botaoMostrarOpcoes;    // Botão principal para mostrar o menu de opções
    public GameObject painelOpcoes;      // Painel que contém os botões de Dica e Anúncio
    public Button botaoDica;             // Botão para ver a primeira dica
    public Button botaoAnuncio;          // Botão para ver o vídeo de anúncio
    public Button botaoFechar;           // Botão para fechar o painel de opções
    public TextMeshProUGUI dicaTexto;    // Texto para exibir a dica
    private bool dicaVisto = false;      // Flag para saber se a dica foi vista
    private bool primeiraDica = true;    // Flag para controlar se é a primeira vez que o jogador está vendo a dica
    private bool botaoDicaVisivel = true; // Flag para controlar a visibilidade do botão de Dica
    private bool botaoAnuncioVisivel = false; // Flag para controlar a visibilidade do botão de Anúncio

    void Awake()
    {
        // Garante que só exista uma instância do DicasManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destrói o objeto se uma instância já existir
            return;
        }

        // Não destrua o DicasManager entre cenas
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Inicialmente o painel de opções está desativado
        painelOpcoes.SetActive(false);

        // Configura os botões
        botaoMostrarOpcoes.onClick.AddListener(MostrarOpcoes);
        botaoDica.onClick.AddListener(ExibirDica);
        botaoAnuncio.gameObject.SetActive(false);  // Inicialmente desativa o botão de anúncio
        botaoFechar.onClick.AddListener(CerrarMenu);
    }

    // Função para mostrar o painel de opções
    void MostrarOpcoes()
    {
        painelOpcoes.SetActive(true);  // Ativa o painel de opções

        // Exibe os botões com base nas flags
        botaoDica.gameObject.SetActive(botaoDicaVisivel);
        botaoAnuncio.gameObject.SetActive(botaoAnuncioVisivel);
    }

    // Função para exibir a dica gratuita
    void ExibirDica()
    {
        // Exibe a dica inicial
        dicaTexto.text = "Dica grátis: Presta atenção nas letras maiúsculas.\n\n" +
                         
                         "Você pode assistir um anúncio para ganhar outra dica.";

        // Marca que a dica foi vista
        dicaVisto = true;
        primeiraDica = false;

        // Esconde o botão de Dica
        botaoDicaVisivel = false; // Marca o botão de Dica como invisível
        botaoAnuncioVisivel = true; // Marca o botão de Anúncio como visível

        // Esconde o botão de Dica
        botaoDica.gameObject.SetActive(botaoDicaVisivel);

        // Mostra o botão de Anúncio (após ver a dica)
        botaoAnuncio.gameObject.SetActive(botaoAnuncioVisivel);
    }

    // Função chamada após assistir o anúncio para atualizar a dica
    public void AtualizarDica()
    {
        dicaTexto.text = "Dica grátis: Presta atenção nas letras maiúsculas.\n\n" +
                         "Dica:Talvez seja algo ETERNO.\n\n";

        // Esconde o botão de Anúncio após o anúncio ser assistido
        botaoAnuncio.gameObject.SetActive(false);
        botaoAnuncioVisivel = false;
    }

    // Função para fechar o menu de opções
    void CerrarMenu()
    {
        painelOpcoes.SetActive(false);  // Desativa o painel de opções
    }
}
