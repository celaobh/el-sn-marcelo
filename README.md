# el-sn-marcelo - Marcelo Peifer de Araujo

Disclaimer:

Antes de começarmos, vamos esclarecer alguns pontos...

Podemos chamar esta entrega de um MVP. Ela não esta com todas as features propostas, mas podemos considerar como uma entrega que agraga bastante valor ao projeto.
As funcionalidades de vitrine, login e cadastro de usuários e cadastros de marca, modelo e veiculos estão presentens, bem como a visualização da vitrine principal,
com todos os veiculos cadastrados, separados por categoria, facilitando a visualização do cliente.
As features de autenticação e autorização também estão contempladas nesta entrega.

Infelizmente não foi possível finalizar o desenvolvimento dentro do timebox proposto, mas acredito que fiz uma entrega de qualidade.

# Vamos ao que interessa

### BACKEND

Como arquitetura, escolhi o padrão Hexagonal.
Tenho certa admirassão por este padrão por facilitar o entendimento do código, mesmo em uma simples corrida de olho nos arquivos.
Claro, alguns vão dizer que poderia atingir o mesmo objetivo com outros padrões, e isso não deixa de ser uma verdade.
Mas, como tinha que escolher um, escolhi o que me sinto mais à vontade.

Sobre organização de código e, por que não, uma excelente boa prática, apliquei um conceito que acho muito importante, o SOC (separation of concerns).
Na camada Application, onde defino os contratos que meus Adapters irão implementar, faço a separação de acordo com a função que cada contrato quer definir:
 - Busca
 - Cadastro
 - Exclusão
 
 e por ai vai.
 
 Essa mesma lógica foi seguida ao implementar o Adapter de comunicação com o banco de dados, que neste momento é minha froteira mais externa com esta aplicação.
 
 #### Mas, Marcelo, qual a vantagem da arquitetura Hexagonal?
 Antes de responder isso, vou voltar um pouco no que já falamos: sempre vão dizer "mas isso você consegue fazer com o padrão X".
 Sim, já sabemos disso. E mesmo assim preferi escolher o padrão Hexagonal, (PONTO !!!)
 
 Se pudermos definir de uma maneira bastante lúdica o que o padrão Hexagonal nos proporciona, é a facilidade que temos ao "plugar" ou "desplugar" uma fronteira da nossa aplicação.
 Digamos assim, hoje estou me comunicando com o SQL Server para gravar e recuperar os dados que esta aplicação consome, então vamos imaginar o seguinte diálogo:
 
 
 SQL SERVER - "Amigo, eu sou o SQL Server e eu vou ter fornecer os dados de todo carro que você quiser!"
 
 
 APLICAÇÂO - "Claro, sem problemas. Mas antes, preciso que você aceite este contrato aqui. Você precisa concordar que, pra falar comigo, você precisa me oferecer meios de buscar, gravar e excluir, OK?!"
 
 
 SQL SERVER - "Moleza!!!"
 
 Tudo resolvido!
 
 Agora, imaginem que o SQL SERVER está ficando velhinho e não tem mais aquela agilidade de antigamento para entregar os dados dos veiculos que meu cliente tanto precisa. Situação chata, né?! Nossa aplicação já esta toda escrita, mudar de fornecedor uma hora dessas pode impactar a minha produção.
 Tempo depois, descobrimos que alguém tinha desenvolvido uma API que se comunicava com um outro banco de dados qualquer e tinha todas as funcionalidades e informações que meu sistema precisa!!!
 A beleza da arquitetura Hexagonal aparece agora; como eu defino contratos para todo tipo de comunicação external que eu faço, uma mudança radical de um BD para uma API nãos erá tão dolorosa.
 Consigo criar um novo adapter, que se comunica com esta nova API utilizando os mesmo contratos já existentes, que eu utilizava com o SQL Server. O resto da minha aplicação não saberá o que aconteceu e o desenvolvimento pesado fica isolado, concentrado em apenas um ponto.
 
 UFA!!!
 
 Claro que esta foi uma explicação muito rápida, mas acho que consegui demonstrar o valor de uma boa organização de código.
 
 
 #### Authentication/Authorization
 
 Para a autenticação usei o JWT para cumprir este objetivo. Faço a procura do usuário com base no login e senha informados. Se existir, faço a criação de um token que representa a identidade deste usuário, adicionando alguns claims à identidade dele, como, por exemplo, qual o role (papel) que ele excerce na aplicação; como : Cliente ou Operador.
 O token gerado é enviado apra o front end, que deve informar ele novamente a cada chamada que ele fizer para a API.
 
 
 A autorização acontece quando determino que determinado método na minha API vai funcionar somente para que tiver autenticado no site e, se que quiser, posso ainda exigir que o usuário só poderá acessar este método se tiver um role igual a OPERADOR, por exemplo.
 
 
 #### HTTPClientFactory
 
 A comunicação via protocolo http/s talvez seja o meio de comunicação mais usado nas aplicações desenvolvidas hoje em dia. Mas é importante saber que essa comunicação não é "de graça".
 Assim como tudo na vida, os sockets são um recurso finito e temos que saber usar para não acabar. Se nossa aplicação é toda baseada em chamadas Http para outras APIs, eventualmente podemos nos deparar com uma situação de "socket exhaustion problem".
 Isso se deu durante muito tempo por não sabermos exatamente o que acontecia quando instanciavamos um HttpClient dentro de um bloco using. Na teoria, o using faz o descarte do HttpClient utilizado, mas o socket continuava aberto em TIME_WAIT. Os servidores utilizam de uma diretiva para saber quanto tempo este socket deve ficar aberto antes de o próprio sistem operacional finalizar aquela comunicação. Ou seja, atravez do código não conseguimos fechar o socket aberto pelo HttpClient.
 Várias sugestões já foram feitas durante os anos, como criar apenas um HttpClient estático para toda a aplicação ou manualmente diminuir o parâmetro utilizado pelo SO para aceleramos o fechamento daquele socket. As duas alternativas acabam por trazer problemas mais severos para a aplicação.
 
 O grande pulo do gato é fazer uso do HTTPClientFactory, onde o .Net faz a governaça dos sockets, trabalhando com um pool de objetos apenas para este fim. Nele você consegue definir quanto tempo sua instância ficará aguardando no pool antes de ser encerrada definitivamente.
 
 
 #### Polly para tratamento de erros transitórios
 
 Posso explicar essa estratégia com uma situação real do dia a dia da LL.
 
 É sabido que temos/tivemos problemas com nosso atual Gateway de API. Por diversas vezes recebemos timeout, por motivos diferentes, e a maioria das vezes não é um problema da aplicação, mas do Gateway.
 Normalmente são erros que acontecem em uma requisição, mas na requisição subsequente tudo funciona normalmente.
 Um jeito elegante de tratar isso (não resolve a causa, já que não sabemos de fato qual o problema) é utilizar de uma politica de retry para erros transitórios.
 Consigo dizer para aplicação que, quando determinado erro acontecer, quero que ele refaça o request por x vezes. Isso ajuda bastante nestas situações, onde eu poderia devolver um erro pro usuário, sendo que foi apenas um erro momentâneo.
 Claro que não podemos ficar fazendo retries infinitos para um erro 401 ou 404, por exemplo, mas se tivermos um timeout, um unauthorized ou até mesmo um erro 500, pode valer a pena fazer uma nova tentativa.
 Juntamente com os retries, é bom adicionarmos um jitter (um numero variável dentro de uma faixa definida) antes de fazermos esta nova solicitação. Isso ajuda a mostrarmos para o servidor que não se trata de um ataque, uma vez que os retries podem acontecer, virtualmente, ao mesmo tempo.
 
 
