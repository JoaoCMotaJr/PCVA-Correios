# PCVA-Correios
Problema do Caixeiro Viajante Assimétrico, nomeado nesse projeto de PCVA.

# Objetivo
Esse projeto tem por objetivo resolver o problema do caixeiro viajante, aplicando as melhores práticas de implementação. Deu-se preferência à legibilidade do que à performance, como representar os vértices por objetos ao invés de tipos primitivos.

# Como executar
## Publicado
A pasta Executable contém o executável e a pasta Files, onde há os inputs e onde é escrito o output. Os arquivos de input devem se chamar:
* encomendas.txt: lista de nomes das cidades de origem e destino para serem resolvidos. Separados por espaço.
* trechos.txt: rotas possíveis entre as cidades, contendo o nome de origem, destino e valor inteiro da distância. Separados por espaço.
Ao executar o .exe é criado o arquivo rotas.txt, com a lista de cidades percorridas e a distância total.
Em caso de exceção, o console irá apresentar a mensagem correspondente.

Para gerar a pasta Executable, basta publicar o projeto PCVA, do tipo console application. 

## Executando no Visual Studio
O código foi feito em .net Core e Visual Studio 2019. A solution possui uma console application que lê da pasta Files os arquivos de entrada e emite a saída na mesma pasta.
Caso seja executado em modo debug, atentar que o caminho desta pasta será "\PCVA-Correios\PCVA\bin\Debug\netcoreapp3.1\Files"

# Método utilizado

Para essa solução, escolheu-se o método de busca em largura de grafos. Esse método garante a otimização do resultado, ao garantir que todos os caminhos possíveis serão percorridos. 
* Parte-se de uma solução não concluída, contendo apenas o ponto inicial (Ex: SF).
* Todas as rotas vizinhas são adicionadas como possíveis soluções (Ex: SF-WS, SF-LS, SF-LV). A solução inacabada (SF) é removida da lista de soluções.
* Para cada uma dessas soluções, o mesmo processo se repete, garantindo que não se retornará para uma cidade já visitada (EX: não é criado a solução SF-WS-SF)
* Para otimizar o tempo de execução, caso uma das soluções já tenha sido obtida até o destino (EX: SF-LV 2) e uma próxima seja descoberta (EX: SF-LS-LV 3), essa segunda solução entrará na lista apenas se ela for menor do que todas as outras já encontradas. (Ex: nesse caso, SF-LS-LV 3 será desconsiderado)
* Caso não seja possível encontrar a cidade no grafo, uma exceção de argumento será lançada.

# Comentários Gerais

## Boas práticas, estilo e comentários
Baseado no livro Clean Code: A Handbook of Agile Software Craftsmanship, Robert C. Martin e os princípios SOLID, as funções foram escritas de maneira legíveis de modo a dispensar comentários. Assim, os comentários se resumem a javadoc das interfaces. Acredito que um código deve ser legível sem comentários, salvo casos extremos ou regras de negócio complexas demais.

## Inversão de dependência
Além da inversão de dependência, para garantir que o código seria reutilizável e desacoplado, optou-se por interfaces com suas próprias class library, conforme sugere o Separated Interface Pattern em https://lostechies.com/derekgreer/2008/12/28/examining-dependency-inversion/.

Como evitou-se utilizar pacotes externos, não foi adicionado um gerenciador de injeção de dependência, nem mesmo o baseado em Microsoft.Extension.


## Ainda mais sobre estilo
O desenvolvimento das classes de PCVASolver foi realizado com TDD. Assim, os testes presentes nos arquivos PCVASolver.UnitTests.SolverTests.cs e PCVASolver.UnitTests.SolverInitializationTests.cs foram produzidos com essa intenção. Os testes demandados que garantem a resolução do problema estão em PCVASolver.UnitTests.SolverSanAndreasTests.cs.

Adotou-se uma estratégia orientada a objetos para montagem do gráfico. Iniciando pela representação do grafo em vértices (City.cs) e arcos (RoadPath.cs). Para representar os caminhos percorridos, temos o objeto Solution.cs. O solution possui, além das cidades já passadas e do custo total, uma flag IsSolved demonstrando se o caminho foi concluído.

Por fim, o PCVAGraphSolver é a classe que implementa a interface IPCVASolver. Ela exige um carregamento prévio dos arcos do grafo (método ConstructGraph), antes de ser resolvido o problema em questão (método ResolveSmallestPath).

Sobre a Entrada e saída, apesar de ter sido feito com a Separated Interface Pattern, nada de muito estiloso foi feito aqui. A interface poderia ser mais genérica, mas não havia necessidade em complicar esse trecho.


# Próximos passos

Além do já implementado, alguns passos poderiam ser executados:
* Renomear as classes do PCVAGraphSolver para a terminologia Vertix e Arc, completando o que foi feito pela interface de usar termos genéricos onde se pode reutilizar o código em outros problemas
* Adicionar um gitignore
* Aumentar a interação com o usuário
* Testar com grafos maiores, apesar de não ter sido o objetivo principal.