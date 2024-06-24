# ControleRemessaModelo
Projeto de controle de envio de remessas para produção de costura 
** Em contrução...

## Esse projeto é autoral e de código aberto!!!

Esse projeto de estuda visa atender uma demande de sistema que realize o controle de envio de remessas de modelos para envio de produção e costura em parceiros externos

-----------------------------------------------

### Resposta da API

	* Sempre que nosso endpoint receber a request com sucesso retornaremos 200. 
	* Porém é importante verificar o corpo da resposta o statuscode, a lista de erros e o bodyresponse.
	* A lista de objetos é o resultado solicitado, ex. uma lista de fações ou um modelo

-----------------------------------------------


Body => 
```json
{
	Success: bool,
	StatusCode: int,
	Errors: List<ErrorMessageResponseAPI>,
	BodyResponse: List<object>
}
```

ErrorMessageResponseAPI =>
```json
{
	Errorcode: int,
	ErrorMessage: string,
}
``` 
