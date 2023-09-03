export type Suspended<T> = {
  result?: T
  error?: Error
}

type SuspensefulProps<T> = {
  data: Suspended<T>
  children: React.ReactNode
  loading: React.ReactNode
  error: React.ReactNode
}

export default function Suspenseful<T>(props: SuspensefulProps<T>) {
  if (props.data.error) return <>{props.error}</>
  else if (!props.data.result) return <>{props.loading}</>
  else return <>{props.children}</>
}
