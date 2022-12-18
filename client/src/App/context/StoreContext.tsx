import { createContext, PropsWithChildren, useContext, useState } from "react";
import { Basket } from "../Models/basket";

// Steps of creating a context:
// 1. Create the context
// 2. Create a hook to use the context
// 3. Create a provider to wrap the children with the context
// 4. Use the hook in the children
// 5. Use the context in the children

interface StoreContextValue {
  basket: Basket | null;
  setBasket: (basket: Basket) => void;
  removeItem: (productId: number, quantity: number) => void;
}

// Create the context with a default value of StoreContextValue | undefined
export const StoreContext = createContext<StoreContextValue | undefined>(
  undefined
);
// Create a hook to use the context
export function useStoreContext() {
  // Get the context
  const context = useContext(StoreContext);

  if (context === undefined)
    throw Error("Oops - we do not seem to be inside the provider!");
  return context;
}

export function StoreProvider({ children }: PropsWithChildren<any>) {
  // This will take care of 2 commands in StoreContextValue (basket and setBasket)
  const [basket, setBasket] = useState<Basket | null>(null);

  // Since there is no Json comming back from the Back-end but a simple status code.,
  // we have to write some logics for the removingItem
  function removeItem(productId: number, quantity: number) {
    if (!basket) return;
    // Create a copy of the items array
    const items = [...basket.items];
    // Find the index of the item with the productId
    const index = items.findIndex((x) => x.productId === productId);
    if (index === -1) return;

    // Reduce the quantity of a product || Remove the quantity from the item
    items[index].quantity -= quantity;
    // If the quantity is less than or equal to 0, remove the item
    if (items[index].quantity <= 0) items.splice(index, 1);

    // Update the basket
    setBasket((prevState) => ({ ...prevState!, items }));
  }

  return (
    // Return the context provider so that the children can access the context using the useStoreContext hook
    <StoreContext.Provider value={{ basket, setBasket, removeItem }}>
      {children}
    </StoreContext.Provider>
  );
}
