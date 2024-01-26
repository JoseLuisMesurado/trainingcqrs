export interface PortalResponse<T> {
  messages: string[] | null;
  response: T;
}