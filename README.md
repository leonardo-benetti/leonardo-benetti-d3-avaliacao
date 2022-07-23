# leonardo-benetti-d3-avaliacao
Projeto de avalia��o para o m�dulo D3 do curso CTEDS.

## Descri��o de funcionamento
Ao iniciar a aplica��o, � observada a tela em que o usu�rio n�o est� logado, tendo as seguintes op��es:
1. Acessar: pede as credenciais de um usu�rio existente (no banco de dados) para acesso. Caso as credenciais passadas n�o sejam v�lidas aparece uma mensagem de erro e retornamos ao estado inicial.
2. Cadastrar novo usu�rio: pede as informa��es de um novo usu�rio (nome, email e senha) para ser cadastrado no banco de dados.
3. Cancelar: encerra a aplica��o.

Depois de cadastrar com sucesso ou fazer o login com sucesso, aparece uma mensagem de boas-vindas com o nome do usu�rio que foi feito o login e novas op��es aparecem:
1. Deslogar: faz o logout do usu�rio atual e volta para o mesmo estado do in�cio da aplica��o.
2. Atualizar dados: pede pelos novos dados de usu�rio (nome, email e senha) que este usu�rio gostaria de ter. Caso queira manter o mesmo nome ou email o usu�rio pode apenas n�o fornecer essas informa��es que o antigo ser� mantido.
3. Deletar seu usu�rio: um aviso de que o usu�rio ser� deletado aparece e � pedido que o usu�rio digite sua senha. Caso ela esteja correta, o usu�rio � deletado e voltamos ao estado inicial da aplica��o.
4. [admin] Listar usu�rios: esta op��o s� aparece para o usu�rio "admin" (que tem um IdUser no DB com valor 1). Ela mostra todos os usu�rios cadastrados no banco (ID, nome e email).
0. Encerrar sistema: encerra a aplica��o

## Reconhecimentos
Embora algumas funcionalidades desenvolvidas ultrapassem um pouco o [escopo](https://github.com/cteds/programacao_avancada_csharp/blob/main/avaliacao/escopo.pdf) fornecido no curso, � importante ressaltar que elas foram feitas da maneira mais simples poss�vel e n�o est�o livres de bugs, principalmente por entradas inv�lidas do usu�rio.

Algumas funcionalidades que seriam importantes nesse sistema que n�o foram implementadas por limita��es de tempo:
- Quando um usu�rio for cadastrado ou atualizado, checar se o email fornecido por ele j� n�o existe no banco de dados antes de realizar a opera��o. De fato, os emails no banco n�o deveriam poder ser repetidos, ent�o at� mesmo a estrutura do banco poderia acomodar essa caracter�stica.
- O usu�rio admin n�o deveria poder ser deletado, pois isso faz com que as funcionalidades exclusivas dele n�o possam ser utilizadas.
- Entradas vazias do usu�rio podem causar erros na execu��o, portanto � recomendado que quem rode esta aplica��o seja "comportado" com as entradas.

V�rios outros pontos de melhoria podem ser levantados, mas creio que para um prot�tipo de aplica��o o funcionamento do programa � satisfat�rio.
