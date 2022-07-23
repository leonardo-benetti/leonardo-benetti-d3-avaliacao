# leonardo-benetti-d3-avaliacao
Projeto de avaliação para o módulo D3 do curso CTEDS.

## Descrição de funcionamento
Ao iniciar a aplicação, é observada a tela em que o usuário não está logado, tendo as seguintes opções:
1. Acessar: pede as credenciais de um usuário existente (no banco de dados) para acesso. Caso as credenciais passadas não sejam válidas aparece uma mensagem de erro e retornamos ao estado inicial.
2. Cadastrar novo usuário: pede as informações de um novo usuário (nome, email e senha) para ser cadastrado no banco de dados.
3. Cancelar: encerra a aplicação.

Depois de cadastrar com sucesso ou fazer o login com sucesso, aparece uma mensagem de boas-vindas com o nome do usuário que foi feito o login e novas opções aparecem:
1. Deslogar: faz o logout do usuário atual e volta para o mesmo estado do início da aplicação.
2. Atualizar dados: pede pelos novos dados de usuário (nome, email e senha) que este usuário gostaria de ter. Caso queira manter o mesmo nome ou email o usuário pode apenas não fornecer essas informações que o antigo será mantido.
3. Deletar seu usuário: um aviso de que o usuário será deletado aparece e é pedido que o usuário digite sua senha. Caso ela esteja correta, o usuário é deletado e voltamos ao estado inicial da aplicação.
4. [admin] Listar usuários: esta opção só aparece para o usuário "admin" (que tem um IdUser no DB com valor 1). Ela mostra todos os usuários cadastrados no banco (ID, nome e email).
0. Encerrar sistema: encerra a aplicação

## Reconhecimentos
Embora algumas funcionalidades desenvolvidas ultrapassem um pouco o [escopo](https://github.com/cteds/programacao_avancada_csharp/blob/main/avaliacao/escopo.pdf) fornecido no curso, é importante ressaltar que elas foram feitas da maneira mais simples possível e não estão livres de bugs, principalmente por entradas inválidas do usuário.

Algumas funcionalidades que seriam importantes nesse sistema que não foram implementadas por limitações de tempo:
- Quando um usuário for cadastrado ou atualizado, checar se o email fornecido por ele já não existe no banco de dados antes de realizar a operação. De fato, os emails no banco não deveriam poder ser repetidos, então até mesmo a estrutura do banco poderia acomodar essa característica.
- O usuário admin não deveria poder ser deletado, pois isso faz com que as funcionalidades exclusivas dele não possam ser utilizadas.
- Entradas vazias do usuário podem causar erros na execução, portanto é recomendado que quem rode esta aplicação seja "comportado" com as entradas.

Vários outros pontos de melhoria podem ser levantados, mas creio que para um protótipo de aplicação o funcionamento do programa é satisfatório.
