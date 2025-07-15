import { getLoggedInUserId } from "./auth";
import { getUserById } from "./UserService";

export async function getUserState() {
    const userId = getLoggedInUserId();
    if (!userId) return { status: "unauthenticated" };

    try {
        const user = await getUserById(userId);
        return user.workoutPlan
            ? { status: "dashboard", user }
            : { status: "chat", user };
    } catch {
        return { status: "unauthenticated" };
    }
}
