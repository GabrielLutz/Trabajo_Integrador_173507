[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/postulante/components/datos-personales/datos-personales.component](../README.md) / DatosPersonalesComponent

# Class: DatosPersonalesComponent

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L28)

## Implements

- `OnInit`
- `OnDestroy`

## Constructors

### Constructor

> **new DatosPersonalesComponent**(`postulanteService`, `router`): `DatosPersonalesComponent`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:38](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L38)

#### Parameters

##### postulanteService

[`PostulanteService`](../../../../../../core/services/postulante.service/classes/PostulanteService.md)

##### router

`Router`

#### Returns

`DatosPersonalesComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:31](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L31)

***

### form

> `readonly` **form**: `FormGroup`\<`DatosPersonalesForm`\>

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:29](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L29)

***

### generos

> `readonly` **generos**: `string`[]

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:35](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L35)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L30)

***

### postulanteId

> **postulanteId**: `number` = `1`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:33](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L33)

***

### router

> `protected` `readonly` **router**: `Router`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:40](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L40)

***

### success

> **success**: `boolean` = `false`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:32](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L32)

## Methods

### cancelar()

> **cancelar**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:241](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L241)

Vuelve al perfil del postulante sin persistir cambios.

#### Returns

`void`

***

### cargarDatos()

> **cargarDatos**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:104](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L104)

Recupera los datos personales del postulante para precargar el formulario.

#### Returns

`void`

***

### getErrorMessage()

> **getErrorMessage**(`fieldName`): `string`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:255](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L255)

Devuelve el mensaje de validación apropiado para cada campo de la UI.

#### Parameters

##### fieldName

`string`

#### Returns

`string`

***

### marcarCamposComoTocados()

> **marcarCamposComoTocados**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:248](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L248)

Fuerza que todos los controles queden en estado touched para mostrar errores.

#### Returns

`void`

***

### ngOnDestroy()

> **ngOnDestroy**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:96](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L96)

Libera las suscripciones reactivas para evitar pérdidas de memoria.

#### Returns

`void`

#### Implementation of

`OnDestroy.ngOnDestroy`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:88](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L88)

Inicializa los suscriptores del formulario y dispara la carga inicial (RF-02).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### onGeneroChange()

> **onGeneroChange**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:147](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L147)

Ajusta las validaciones cuando se selecciona el género "Otro".

#### Returns

`void`

***

### onSubmit()

> **onSubmit**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:186](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L186)

Envía el formulario y coordina la navegación posterior al éxito (RF-02).

#### Returns

`void`

***

### validarCedula()

> **validarCedula**(): `void`

Defined in: [src/app/features/postulante/components/datos-personales/datos-personales.component.ts:162](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/datos-personales/datos-personales.component.ts#L162)

Dispara la validación remota de cédula según RF-20.

#### Returns

`void`
