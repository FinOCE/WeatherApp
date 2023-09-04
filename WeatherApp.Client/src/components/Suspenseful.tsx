export type Suspended<T> = {
  result?: T
  error?: Error
}

type SuspensefulProps<T> = {
  data: Suspended<T> | Suspended<any>[]
  children: React.ReactNode
  loading: React.ReactNode
  error: React.ReactNode
}

export default function Suspenseful<T>(props: SuspensefulProps<T>) {
  if (
    Array.isArray(props.data)
      ? props.data.some(data => data.error)
      : props.data.error
  )
    return <>{props.error}</>
  else if (
    Array.isArray(props.data)
      ? !props.data.every(data => data.result)
      : !props.data.result
  )
    return <>{props.loading}</>
  else return <>{props.children}</>
}
