import { clear } from "../../lib/storage"
import { RootStore } from "../../models/root-store"

export type GetRootStore = () => RootStore

export const withCustomActions = (getRootStore: GetRootStore) => {
  return tron => {
    return {
      onCommand: async command => {
        if (command.type !== "custom") return
        switch (command.payload) {
          case "resetStore":
            console.tron.log("clearing store")
            clear()
            break
          case "resetNavigation":
            console.tron.log("resetting navigation store")
            getRootStore().navigationStore.reset()
            break
        }
      },
    }
  }
}
