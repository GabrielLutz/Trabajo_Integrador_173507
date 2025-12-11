[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/tribunal/generar-ordenamiento/generar-ordenamiento.component](../README.md) / GenerarOrdenamientoComponent

# Class: GenerarOrdenamientoComponent

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L15)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new GenerarOrdenamientoComponent**(`route`, `router`, `fb`, `tribunalService`): `GenerarOrdenamientoComponent`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:36](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L36)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### fb

`NonNullableFormBuilder`

##### tribunalService

[`TribunalService`](../../../../../core/services/tribunal.service/classes/TribunalService.md)

#### Returns

`GenerarOrdenamientoComponent`

## Properties

### cargandoPreview

> **cargandoPreview**: `boolean` = `false`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:34](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L34)

***

### error

> **error**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:29](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L29)

***

### form

> **form**: `FormGroup`\<\{ `aplicarCuotas`: `FormControl`\<`boolean`\>; `esDefinitivo`: `FormControl`\<`boolean`\>; `puntajeMinimoAprobacion`: `FormControl`\<`number`\>; \}\>

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L17)

***

### generando

> **generando**: `boolean` = `false`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L28)

***

### llamadoId

> **llamadoId**: `number`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L16)

***

### pasoActual

> **pasoActual**: `number` = `1`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:24](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L24)

***

### previewInscripciones

> **previewInscripciones**: [`InscripcionParaEvaluar`](../../../../../core/models/tribunal.models/interfaces/InscripcionParaEvaluar.md)[] = `[]`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:33](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L33)

***

### resultado

> **resultado**: `any` = `null`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L30)

***

### totalPasos

> **totalPasos**: `number` = `3`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:25](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L25)

## Accessors

### aplicarCuotas

#### Get Signature

> **get** **aplicarCuotas**(): `boolean`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:181](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L181)

Indica si se deben aplicar cuotas en la generación del ordenamiento.

##### Returns

`boolean`

***

### esDefinitivo

#### Get Signature

> **get** **esDefinitivo**(): `boolean`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:188](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L188)

Indica si el ordenamiento a generar es definitivo.

##### Returns

`boolean`

***

### puntajeMinimoAprobacion

#### Get Signature

> **get** **puntajeMinimoAprobacion**(): `number`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:174](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L174)

Puntaje mínimo ingresado en el formulario.

##### Returns

`number`

## Methods

### cargarPreview()

> **cargarPreview**(): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:62](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L62)

Obtiene inscripciones evaluadas para mostrar un preview antes de generar el ordenamiento.

#### Returns

`void`

***

### esAprobado()

> **esAprobado**(`inscripcion`): `boolean`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:195](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L195)

Determina si la inscripción supera el umbral para ser considerada aprobada.

#### Parameters

##### inscripcion

[`InscripcionParaEvaluar`](../../../../../core/models/tribunal.models/interfaces/InscripcionParaEvaluar.md)

#### Returns

`boolean`

***

### generarOrdenamiento()

> **generarOrdenamiento**(): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:103](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L103)

Invoca el endpoint de generación de ordenamiento aplicando los parámetros elegidos (RF-14).

#### Returns

`void`

***

### getCantidadAprobados()

> **getCantidadAprobados**(): `number`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:151](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L151)

Cantidad de postulantes que superan el puntaje mínimo configurado.

#### Returns

`number`

***

### getCantidadPorUniverso()

> **getCantidadPorUniverso**(`universo`): `number`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:159](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L159)

Cantidad de aprobados por universo de acción afirmativa para los indicadores.

#### Parameters

##### universo

`"afro"` | `"trans"` | `"disc"`

#### Returns

`number`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:52](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L52)

Recupera el llamado desde la ruta y carga la previsualización inicial (RF-14).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### pasoAnterior()

> **pasoAnterior**(): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:94](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L94)

Retrocede un paso en el asistente.

#### Returns

`void`

***

### siguientePaso()

> **siguientePaso**(): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:85](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L85)

Avanza al siguiente paso del asistente.

#### Returns

`void`

***

### verOrdenamiento()

> **verOrdenamiento**(`ordenamientoId`): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:137](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L137)

Navega al detalle del ordenamiento recién generado.

#### Parameters

##### ordenamientoId

`number`

#### Returns

`void`

***

### volver()

> **volver**(): `void`

Defined in: [src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts:144](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/generar-ordenamiento/generar-ordenamiento.component.ts#L144)

Regresa al dashboard del tribunal.

#### Returns

`void`
