import './index.scss'
import classNames from 'classnames'

const CuSvgIcon = (props) => {
  const { iconClass, className } = props
  const cns = classNames(
    'cu-svg-icon',
    className
  )
  const iconName = `#icon-${iconClass}`
  return (
    <svg
      aria-hidden="true"
      className={cns}
    >
      <use xlinkHref={iconName} />
    </svg>
  )
}

export default CuSvgIcon
