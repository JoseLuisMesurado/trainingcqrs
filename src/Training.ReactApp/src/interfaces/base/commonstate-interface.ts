import { ErrorInterface } from "./error-interface"

export interface CommonStateInterface {
    isLoading: boolean
    isSuccessful: boolean
    error: ErrorInterface
}