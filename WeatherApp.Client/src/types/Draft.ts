export type Draft<T> = {
  published: T
  draft: Record<keyof T, string>
}
