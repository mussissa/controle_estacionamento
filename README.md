# controle_estacionamento

Controle Estacionamento

Este programa foi construido sem nenhuma finalidade comercial, seu objetivo primordial é
servir de base avaliatoria de conhecimentos.
A estrutura principal do é em MVC realizado em c#
A base de dados possui duas tabelas e foi criada em SQL SERVER. A vigencia_valor que é tabela de parametizações
responsavel por repassar a informaçao dos valores vigente que serão cobrados e a controle_estacionamento
que é a tabela responsavel por guardar as informaçoes de entrada e saida do veiculo.
Foi utilizado o entity framework como a ponto de ligação entre o modelo de negocio e os codigos.
Foi necessario criar um modelo para mapear cada tabela, assim como controllers e views.

O controller vigencia_valorController é o responsavel pela operação das parametizações e possui entre as actions destaca-se a 
action responsavel pela inclusão que é a Create. Onde esta faz a manipulaçao das datas invertendo e tambem faz a validaçao
da inclusao, caso a data final seja menor que a data inicial a inclusao nao é realizda e continua na tela de criação
ate cancelar ou corrigir a data.

O controller controle_estacionamentoController é o responsavel por toda a gravaçao dos carros no estacionamento, cujo a 
chave de consulta é placa dos carros. Neste destaca-se a action create onde repassa a data para a view no formato dd:MM:yyyy HH:mm:ss
a action create faz a validaçoes e realiza os calculos de tolerancia de tempo no caso abaixo de 10 min o valor continua igual,
a cima de 10 min o valor sofre alteração de acordo com os valores adicionais cadastrado na tabela de parametização vigencia_valor.

Os registro não estao sendo descartados, preferir manter os dados listados pois se fosse deletados quando o carro sair do 
estacionamento seria perdido o registro do mesmo. Neste caso quando o carro sai do estacionamento ele fica com o botao de marcar
saida desabilitado evitando que o usuario tente sair duas vezes seguidamente.

