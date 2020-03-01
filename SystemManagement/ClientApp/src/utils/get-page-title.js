import defaultSettings from '@/settings'

const title = defaultSettings.title || 'XXX 管理系统系统'

export default function getPageTitle(pageTitle) {
  if (pageTitle) {
    return `${pageTitle} - ${title}`
  }
  return `${title}`
}
